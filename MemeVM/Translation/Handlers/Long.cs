using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Long : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Ldc_I8 };
        public VMOpCode Output => VMOpCode.Int64;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            success = true;
            return new VMInstruction(VMOpCode.Int32, (long)method.Body.Instructions[index].Operand);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var buf = new byte[9];
            buf[0] = (byte)VMOpCode.Int64;

            Array.Copy(BitConverter.GetBytes((long)instruction.Operand), 0, buf, 1, 8);
            return buf;
        }
    }
}
