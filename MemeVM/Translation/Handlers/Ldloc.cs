using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Ldloc : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Ldloc };
        public VMOpCode Output => VMOpCode.Ldloc;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            var loc = (short)method.Body.Variables.IndexOf((Local)method.Body.Instructions[index].Operand);
            success = true;
            return new VMInstruction(VMOpCode.Ldloc, loc);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var buf = new byte[3];
            buf[0] = (byte)VMOpCode.Ldloc;
            Array.Copy(BitConverter.GetBytes((short)instruction.Operand), 0, buf, 1, 2);
            return buf;
        }
    }
}
