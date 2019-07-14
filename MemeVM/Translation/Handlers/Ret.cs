using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Ret : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Ret };
        public VMOpCode Output => VMOpCode.Ret;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            success = true;
            return new VMInstruction(VMOpCode.Ret);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            return new[] { (byte)VMOpCode.Ret };
        }
    }
}
