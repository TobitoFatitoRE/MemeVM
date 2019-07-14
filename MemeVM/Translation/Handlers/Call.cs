using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    //TODO: Generic methods...
    class Call : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Call, OpCodes.Callvirt, OpCodes.Newobj };
        public VMOpCode Output => VMOpCode.Call;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            var operand = method.Body.Instructions[index].Operand;
            if (operand is MethodSpec) {
                success = false;
                return new VMInstruction(VMOpCode.UNUSED);
            }

            var target = ((IMethod)operand).ResolveMethodDef();
            var fqname = target.Module.Assembly.FullName;
            if (!body.References.Contains(fqname))
                body.References.Add(fqname);

            success = true;
            return new VMInstruction(VMOpCode.Call, new Tuple<short, MethodDef>((short)body.References.IndexOf(fqname), target));
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var buf = new byte[8];
            buf[0] = (byte)VMOpCode.Call;
            var (referenceId, method) = (Tuple<short, MethodDef>)instruction.Operand;

            if (!body.Translated.ContainsKey(method)) {
                Array.Copy(BitConverter.GetBytes(referenceId), 0, buf, 1, 2);
                Array.Copy(BitConverter.GetBytes(TokenGetter.GetMdToken(method)), 0, buf, 3, 4);
                buf[7] = 0;
                return buf;
            }

            Array.Copy(BitConverter.GetBytes((short)body.MethodToIndex[method]), 0, buf, 1, 2);
            Array.Copy(BitConverter.GetBytes(method.Parameters.Count), 0, buf, 3, 4);
            buf[7] = 1;
            return buf;
        }
    }
}
