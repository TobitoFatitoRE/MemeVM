using System.IO;
using MemeVM.Runtime.Engine;

namespace MemeVM.Runtime.Handlers {
    class Dup : IHandler {
        public OpCode Handles => OpCode.Dup;
        public void Handle(VM machine, Body body, Instruction instruction) {
            var value = machine.Stack.Pop();
            machine.Stack.Push(value);
            machine.Stack.Push(value);
        }

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Dup);
    }
}
