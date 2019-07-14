using System.IO;
using MemeVM.Runtime.Engine;

namespace MemeVM.Runtime {
    interface IHandler {
        OpCode Handles { get; }
        void Handle(VM machine, Body body, Instruction instruction);
        Instruction Deserialize(BinaryReader reader);
    }
}
