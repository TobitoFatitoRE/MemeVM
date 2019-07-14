using System.IO;
using MemeVM.Runtime.Engine;

namespace MemeVM.Runtime.Handlers {
    class Long : IHandler {
        public OpCode Handles => OpCode.Int64;
        public void Handle(VM machine, Body body, Instruction instruction) =>
            machine.Stack.Push(instruction.Operand);

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Int64, reader.ReadInt64());
    }
}
