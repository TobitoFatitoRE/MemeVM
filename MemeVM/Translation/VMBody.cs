using System;
using System.Collections.Generic;
using System.Text;
using dnlib.DotNet;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation {
    class VMBody {
        internal VMBody() {
            References = new List<string>();
            MethodToIndex = new Dictionary<MethodDef, int>();
            Translated = new Dictionary<MethodDef, List<VMInstruction>>();
            OffsetHelper = new Offsets();
        }

        internal List<string> References;
        internal Dictionary<MethodDef, int> MethodToIndex;
        internal Dictionary<MethodDef, List<VMInstruction>> Translated;
        internal Offsets OffsetHelper;

        internal byte[] Serialize() {
            var arr = new List<byte>();

            var rCount = References.Count;
            arr.AddRange(BitConverter.GetBytes(rCount));
            foreach (var reference in References) {
                arr.AddRange(BitConverter.GetBytes(reference.Length));
                arr.AddRange(Encoding.UTF8.GetBytes(reference));
            }

            var mCount = Translated.Count;
            arr.AddRange(BitConverter.GetBytes(mCount));
            foreach (var method in Translated.Values) {
                arr.AddRange(BitConverter.GetBytes(method.Count));
                foreach (var instruction in method) {
                    arr.AddRange(Map.Lookup(instruction.OpCode).Serialize(this, instruction, OffsetHelper));
                }
            }

            return arr.ToArray();
        }
    }
}
