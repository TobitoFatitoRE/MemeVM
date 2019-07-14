using System.IO;
using MemeVM.Runtime.Engine;

namespace MemeVM.Runtime.Handlers {
    class Null : IHandler {
        public OpCode Handles => OpCode.Null;
        public void Handle(VM machine, Body body, Instruction instruction) =>
            machine.Stack.Push(null);

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Null);
    }
}
