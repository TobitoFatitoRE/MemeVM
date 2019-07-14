using System.IO;
using MemeVM.Runtime.Engine;

namespace MemeVM.Runtime.Handlers {
    class Ldarg : IHandler {
        public OpCode Handles => OpCode.Ldarg;
        public void Handle(VM machine, Body body, Instruction instruction) =>
            machine.Stack.Push(machine.Parameters[(short)instruction.Operand]);

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Ldarg, reader.ReadInt16());
    }
}
