using System.IO;
using MemeVM.Runtime.Engine;

namespace MemeVM.Runtime.Handlers {
    class Pop : IHandler {
        public OpCode Handles => OpCode.Pop;
        public void Handle(VM machine, Body body, Instruction instruction) =>
            machine.Stack.Pop();

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Pop);
    }
}
