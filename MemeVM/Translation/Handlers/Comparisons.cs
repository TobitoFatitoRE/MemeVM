using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Ceq : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Ceq };
        public VMOpCode Output => VMOpCode.Cmp;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            success = true;
            return new VMInstruction(VMOpCode.Cmp);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) =>
            new[] { (byte)VMOpCode.Cmp };
    }

    class Cgt : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Cgt };
        public VMOpCode Output => VMOpCode.Cgt;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            success = true;
            return new VMInstruction(VMOpCode.Cgt);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) =>
            new[] { (byte)VMOpCode.Cgt };
    }

    class Clt : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Clt };
        public VMOpCode Output => VMOpCode.Clt;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            success = true;
            return new VMInstruction(VMOpCode.Clt);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) =>
            new[] { (byte)VMOpCode.Clt };
    }
}
