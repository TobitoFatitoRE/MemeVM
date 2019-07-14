using System;
using System.IO;
using MemeVM.Runtime.Engine;

namespace MemeVM.Runtime.Handlers {
    class Ldfld : IHandler {
        public OpCode Handles => OpCode.Ldfld;
        public void Handle(VM machine, Body body, Instruction instruction) {
            var id = ((Tuple<short, int>)instruction.Operand).Item1;
            var token = ((Tuple<short, int>)instruction.Operand).Item2;

            var asm = body.GetReference(id);
            var field = asm.ManifestModule.ResolveField(token);
            var obj = field.IsStatic ? null : machine.Stack.Pop();
            machine.Stack.Push(field.GetValue(obj));
        }

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Ldfld, new Tuple<short, int>(reader.ReadInt16(), reader.ReadInt32()));
    }
}
