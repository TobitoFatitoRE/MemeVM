using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Dup : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Dup };
        public VMOpCode Output => VMOpCode.Dup;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            success = true;
            return new VMInstruction(VMOpCode.Dup);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) =>
            new[] { (byte)VMOpCode.Dup };
    }
}
