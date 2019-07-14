using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation.Handlers {
    //TODO: Generics
    class Newarr : IHandler {
        public OpCode[] Translates => new[] { OpCodes.Newarr };
        public VMOpCode Output => VMOpCode.Newarr;
        public VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success) {
            var type = ((ITypeDefOrRef)method.Body.Instructions[index].Operand).ResolveTypeDef();
            if (type == null) {
                success = false;
                return new VMInstruction(VMOpCode.UNUSED);
            }

            var fqname = type.Module.Assembly.FullName;
            if (!body.References.Contains(fqname))
                body.References.Add(fqname);

            success = true;
            return new VMInstruction(VMOpCode.Newarr, new Tuple<short, TypeDef>((short)body.References.IndexOf(fqname), type));
        }

        public byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper) {
            var buf = new byte[7];
            buf[0] = (byte)VMOpCode.Newarr;
            var (referenceid, type) = (Tuple<short, TypeDef>)instruction.Operand;
            Array.Copy(BitConverter.GetBytes(referenceid), 0, buf, 1, 2);
            Array.Copy(BitConverter.GetBytes(TokenGetter.GetMdToken(type)), 0, buf, 3, 4);
            return buf;
        }
    }
}
