using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MemeVM.Runtime.Engine;

namespace MemeVM.Runtime.Handlers {
    //TODO: Generic methods
    class Call : IHandler {
        public OpCode Handles => OpCode.Call;
        public void Handle(VM machine, Body body, Instruction instruction) {
            var op = (Tuple<short, int, bool>)instruction.Operand;

            if (!op.Item3) {
                HandleNormal(machine, body, instruction);
                return;
            }

            var index = op.Item1;
            var pcount = op.Item2;

            var parameters = new object[pcount];
            for (var i = parameters.Length - 1; i >= 0; i--)
                parameters[i] = machine.Stack.Pop();

            var res = Dispatcher.Run(body.CurrentAssembly, index, parameters);
            if (!(res is NoMoreStackItem))
                machine.Stack.Push(res);
        }

        static void HandleNormal(VM machine, Body body, Instruction instruction) {
            var op = (Tuple<short, int, bool>)instruction.Operand;

            var asm = body.GetReference(op.Item1);
            var info = asm.ManifestModule.ResolveMember(op.Item2);
            var target = asm.ManifestModule.ResolveMethod(op.Item2);

            var rawparams = target.GetParameters();
            var paramscount = rawparams.Length;
            var parameters = new object[paramscount];
            for (var i = parameters.Length - 1; i >= 0; i--)
                parameters[i] = machine.Stack.Pop();

            if ((info.MemberType & MemberTypes.Constructor) == MemberTypes.Constructor) {
                var ctorinfo = (ConstructorInfo)info;
                machine.Stack.Push(ctorinfo.Invoke(parameters));
                return;
            }

            var methodinfo = (MethodInfo)info;
            var parent = methodinfo.IsStatic ? null : machine.Stack.Pop();
            var res = target.Invoke(parent, parameters);
            if (methodinfo.ReturnType != typeof(void))
                machine.Stack.Push(res);
        }

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Call, new Tuple<short, int, bool>(reader.ReadInt16(), reader.ReadInt32(), reader.ReadBoolean()));
    }
}
