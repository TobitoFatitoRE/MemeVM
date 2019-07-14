using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Useless : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Nop, OpCodes.Break };
        public VMOpCode Output => VMOpCode.UNUSED;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            helper.Add(index, -1);
            success = true;
            return new VMInstruction(VMOpCode.UNUSED);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) =>
            Array.Empty<byte>();
    }
}
