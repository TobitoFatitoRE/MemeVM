using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Ldstr : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Ldstr };
        public VMOpCode Output => VMOpCode.String;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            var operand = (string)method.Body.Instructions[index].Operand;

            success = true;
            return new VMInstruction(VMOpCode.String, operand);
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var str = Encoding.UTF8.GetBytes((string)instruction.Operand);
            var buf = new byte[5 + str.Length];
            buf[0] = (byte)VMOpCode.String;
            Array.Copy(BitConverter.GetBytes(str.Length), 0, buf, 1, 4);
            Array.Copy(str, 0, buf, 5, str.Length);
            return buf;
        }
    }
}
