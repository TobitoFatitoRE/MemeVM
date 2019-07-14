using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Int : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Ldc_I4 };
        public VMOpCode Output => VMOpCode.Int32;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            success = true;
            return new VMInstruction(VMOpCode.Int32, (int)method.Body.Instructions[index].Operand);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var buf = new byte[5];
            buf[0] = (byte)VMOpCode.Int32;
            Array.Copy(BitConverter.GetBytes((int)instruction.Operand), 0, buf, 1, 4);

            return buf;
        }
    }
}
