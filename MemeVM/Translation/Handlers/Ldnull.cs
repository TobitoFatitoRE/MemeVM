using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Ldnull : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Ldnull };
        public VMOpCode Output => VMOpCode.Null;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            success = true;
            return new VMInstruction(VMOpCode.Null);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var buf = new byte[1];
            buf[0] = (byte)VMOpCode.Null;
            return buf;
        }
    }
}
