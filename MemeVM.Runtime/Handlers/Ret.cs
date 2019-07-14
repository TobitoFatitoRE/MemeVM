using System.IO;
using MemeVM.Runtime.Engine;

namespace MemeVM.Runtime.Handlers {
    class Ret : IHandler {
        public OpCode Handles => OpCode.Ret;
        public void Handle(VM machine, Body body, Instruction instruction) =>
            machine.State = VMState.Return;

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Ret);
    }
}
