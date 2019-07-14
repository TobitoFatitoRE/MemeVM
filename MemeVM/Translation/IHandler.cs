using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation {
    interface IHandler {
        OpCode[] Translates { get; }
        VMOpCode Output { get; }
        VMInstruction Translate(VMBody body, MethodDef method, int index, Offsets helper, out bool success);
        byte[] Serialize(VMBody body, VMInstruction instruction, Offsets helper);
    }
}
