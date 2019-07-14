using System.IO;
using MemeVM.Runtime.Engine;

namespace MemeVM.Runtime.Handlers {
    class Ldloc : IHandler {
        public OpCode Handles => OpCode.Ldloc;
        public void Handle(VM machine, Body body, Instruction instruction) =>
            machine.Stack.Push(machine.Locals.Get((short)instruction.Operand));

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Ldloc, reader.ReadInt16());
    }
}
