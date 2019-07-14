using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    class Stfld : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Stfld, OpCodes.Stsfld };
        public VMOpCode Output => VMOpCode.Stfld;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            var op = ((IField)method.Body.Instructions[index].Operand).ResolveFieldDef();
            if (op == null) {
                success = false;
                return new VMInstruction(VMOpCode.UNUSED);
            }

            var fqname = op.Module.Assembly.FullName;
            if (!body.References.Contains(fqname))
                body.References.Add(fqname);

            success = true;
            return new VMInstruction(VMOpCode.Stfld, new Tuple<short, FieldDef>((short)body.References.IndexOf(fqname), op));
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var buf = new byte[7];
            buf[0] = (byte)VMOpCode.Stfld;
            var (refid, field) = (Tuple<short, FieldDef>)instruction.Operand;
            Array.Copy(BitConverter.GetBytes(refid), 0, buf, 1, 2);
            Array.Copy(BitConverter.GetBytes(TokenGetter.GetMdToken(field)), 0, buf, 3, 4);
            return buf;
        }
    }
}
