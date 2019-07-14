using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Br : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Br };
        public VMOpCode Output => VMOpCode.Jmp;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            var operand = method.Body.Instructions.IndexOf((Instruction)method.Body.Instructions[index].Operand);
            success = true;
            return new VMInstruction(VMOpCode.Jmp, operand);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var buf = new byte[5];
            buf[0] = (byte)VMOpCode.Jmp;
            Array.Copy(BitConverter.GetBytes(helper.Get((int)instruction.Operand)), 0, buf, 1, 4);
            return buf;
        }
    }

    class Brtrue : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Brtrue };
        public VMOpCode Output => VMOpCode.Jt;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            var operand = method.Body.Instructions.IndexOf((Instruction)method.Body.Instructions[index].Operand);
            success = true;
            return new VMInstruction(VMOpCode.Jt, operand);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var buf = new byte[5];
            buf[0] = (byte)VMOpCode.Jt;
            Array.Copy(BitConverter.GetBytes(helper.Get((int)instruction.Operand)), 0, buf, 1, 4);
            return buf;
        }
    }

    class Brfalse : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Brfalse };
        public VMOpCode Output => VMOpCode.Jf;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            var operand = method.Body.Instructions.IndexOf((Instruction)method.Body.Instructions[index].Operand);
            success = true;
            return new VMInstruction(VMOpCode.Jf, operand);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var buf = new byte[5];
            buf[0] = (byte)VMOpCode.Jf;
            Array.Copy(BitConverter.GetBytes(helper.Get((int)instruction.Operand)), 0, buf, 1, 4);
            return buf;
        }
    }

    class Beq : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Beq };
        public VMOpCode Output => VMOpCode.Je;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            success = true;
            return new VMInstruction(VMOpCode.Je, method.Body.Instructions.IndexOf(method.Body.Instructions[index]));
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var buf = new byte[5];
            buf[0] = (byte)VMOpCode.Je;
            Array.Copy(BitConverter.GetBytes(helper.Get((int)instruction.Operand)), 0, buf, 1, 4);
            return buf;
        }
    }
}
