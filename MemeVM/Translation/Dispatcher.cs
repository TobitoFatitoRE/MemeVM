using System.Collections.Generic;
using dnlib.DotNet;
using MemeVM.Translation.Helpers;

namespace MemeVM.Translation {
    static class Dispatcher {
        internal static List<VMInstruction> TranslateMethod(VMBody body, MethodDef method) {
            var list = new List<VMInstruction>();

            for (var i = 0; i < method.Body.Instructions.Count; i++) {
                var translator = Map.Lookup(method.Body.Instructions[i].OpCode);
                if (translator == null)
                    return null;

                var res = translator.Translate(body, method, i, body.OffsetHelper, out var good);
                if (res.OpCode != VMOpCode.UNUSED) list.Add(res);
                if (!good)
                    return null;
            }

            //TODO: Exception handlers

            return list;
        }
    }
}
