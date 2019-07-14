using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Pop : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Pop };
        public VMOpCode Output => VMOpCode.Pop;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            success = true;
            return new VMInstruction(VMOpCode.Pop);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var buf = new byte[1];
            buf[0] = (byte)VMOpCode.Pop;
            return buf;
        }
    }
}
