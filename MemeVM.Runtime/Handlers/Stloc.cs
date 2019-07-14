using System.IO;
using MemeVM.Runtime.Engine;

namespace MemeVM.Runtime.Handlers {
    class Stloc : IHandler {
        public OpCode Handles => OpCode.Stloc;
        public void Handle(VM machine, Body body, Instruction instruction) =>
            machine.Locals.Set((short) instruction.Operand, machine.Stack.Pop());

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Stloc, reader.ReadInt16());
    }
}
