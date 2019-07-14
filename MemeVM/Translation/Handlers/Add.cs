using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Add : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Add, OpCodes.Add_Ovf, OpCodes.Add_Ovf_Un };
        public VMOpCode Output => VMOpCode.Add;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            success = true;
            return new VMInstruction(VMOpCode.Add);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            return new[] { (byte)VMOpCode.Add };
        }
    }
}
