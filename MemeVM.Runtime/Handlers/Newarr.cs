using System;
using System.IO;
using MemeVM.Runtime.Engine;

namespace MemeVM.Runtime.Handlers {
    class Newarr : IHandler {
        public OpCode Handles => OpCode.Newarr;
        public void Handle(VM machine, Body body, Instruction instruction) {
            var tuple = (Tuple<short, int>)instruction.Operand;

            var refid = tuple.Item1;
            var token = tuple.Item2;

            var asm = body.GetReference(refid).ManifestModule;
            var type = asm.ResolveType(token);
            var length = (int)machine.Stack.Pop();

            machine.Stack.Push(Array.CreateInstance(type, length));
        }

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Newarr, new Tuple<short, int>(reader.ReadInt16(), reader.ReadInt32()));
    }
}
