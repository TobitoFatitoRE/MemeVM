using System.IO;
using System.IO.Compression;
using System.Linq;
using Confuser.Core;
using Confuser.Core.Services;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;
using MemeVM.Translation;
using MemeVM.Translation.Helpers;

namespace MemeVM {
    public class VirtualizatonPhase : ProtectionPhase {
        public VirtualizatonPhase(ConfuserComponent parent) : base(parent) { }
        public override string Name => "MemeVM.Virtualization";
        public override ProtectionTargets Targets => ProtectionTargets.Methods;

        protected override void Execute(ConfuserContext context, ProtectionParameters parameters) {
            if (!parameters.Targets.Any())
                return;

            context.CurrentModuleWriterListener.OnWriterEvent += InsertVMBodies;

            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
            foreach (MethodDef method in parameters.Targets.WithProgress(context.Logger)) {
                if (!method.HasBody || method.DeclaringType.IsGlobalModuleType || method.Body.HasExceptionHandlers)
                    continue;

                var module = method.Module;
                
                if (!Context.Bodies.ContainsKey(module))
                    Context.Bodies.Add(module, new VMBody());

                var translated = Dispatcher.TranslateMethod(Context.Bodies[module], method);
                if (translated == null)
                    continue;
                
                Context.Bodies[module].Translated.Add(method, translated);
                Context.Bodies[module].MethodToIndex.Add(method, Context.Bodies[module].Translated.Count - 1);
                context.CheckCancellation();
            }

            foreach (var pair in Context.Bodies) {
                if (pair.Value.Translated.Count < 1)
                    continue;

                var target = pair.Key.Import(Context.Entry);
                foreach (var translated in pair.Value.Translated.WithProgress(context.Logger)) {
                    var method = translated.Key;
                    method.Body = new CilBody { MaxStack = 1 };
                    var body = method.Body.Instructions;

                    body.Add(OpCodes.Ldtoken.ToInstruction(method.DeclaringType));
                    body.Add(OpCodes.Ldc_I4.ToInstruction(pair.Value.MethodToIndex[method]));

                    AddParameters(method);

                    var genericType = method.ReturnType == method.Module.CorLibTypes.Void ? target.DeclaringType.ToTypeSig() : method.ReturnType;
                    var sig = new MethodSpecUser((MemberRef)target, new GenericInstMethodSig(genericType));
                    body.Add(OpCodes.Call.ToInstruction(sig));

                    if (method.ReturnType == method.Module.CorLibTypes.Void)
                        body.Add(OpCodes.Pop.ToInstruction());

                    body.Add(OpCodes.Ret.ToInstruction());
                    context.CheckCancellation();
                }
            }

            Context.RuntimeModule.Dispose();
        }

        static void InsertVMBodies(object sender, ModuleWriterListenerEventArgs e) {
            var writer = (ModuleWriterBase)sender;
            if (e.WriterEvent != ModuleWriterEvent.MDMemberDefRidsAllocated)
                return;

            TokenGetter.Writer = writer;

            var body = Context.Bodies[writer.Module];
            var data = body.Serialize();
            writer.Module.Resources.Add(new EmbeddedResource(" ", Compress(data)));
        }

        static byte[] Compress(byte[] array) {
            using (var ms = new MemoryStream()) {
                using (var def = new DeflateStream(ms, CompressionLevel.Optimal)) {
                    def.Write(array, 0, array.Length);
                }

                return ms.ToArray();
            }
        }

        static void AddParameters(MethodDef method) {
            if (method.Parameters.Count == 0) {
                method.Body.Instructions.Add(OpCodes.Ldnull.ToInstruction());
                return;
            }

            method.Body.Instructions.Add(OpCodes.Ldc_I4.ToInstruction(method.Parameters.Count));
            method.Body.Instructions.Add(OpCodes.Newarr.ToInstruction(method.Module.CorLibTypes.Object));
            method.Body.Instructions.Add(OpCodes.Dup.ToInstruction());

            for (var i = 0; i < method.Parameters.Count; i++) {
                method.Body.Instructions.Add(OpCodes.Ldc_I4.ToInstruction(i));
                method.Body.Instructions.Add(OpCodes.Ldarg.ToInstruction(method.Parameters[i]));

                var cor = method.Module.CorLibTypes;
                var param = method.Parameters[i];
                if (!param.IsHiddenThisParameter) {
                    if (param.Type != cor.String && param.Type != cor.Object && param.Type != cor.TypedReference) {
                        var spec = new TypeSpecUser(param.Type);
                        method.Body.Instructions.Add(new Instruction(OpCodes.Box, spec));
                    }
                }

                method.Body.Instructions.Add(OpCodes.Stelem_Ref.ToInstruction());
                method.Body.Instructions.Add(OpCodes.Dup.ToInstruction());
            }

            method.Body.Instructions.Remove(method.Body.Instructions.Last());
        }
    }
}
