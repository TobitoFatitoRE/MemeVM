using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using MemeVM.Runtime.Helpers;

namespace MemeVM.Runtime {
    class Body {
        internal Body(Stream resourceStream) {
            _references = new Dictionary<string, Assembly>();
            _methods = new List<List<Instruction>>();

            using (var def = new DeflateStream(resourceStream, CompressionMode.Decompress)) {
                using (var reader = new BinaryReader(def)) {
                    var rCount = reader.ReadInt32();
                    for (var i = 0; i < rCount; i++) {
                        var len = reader.ReadInt32();
                        _references.Add(Encoding.UTF8.GetString(reader.ReadBytes(len)), null);
                    }

                    var mCount = reader.ReadInt32();
                    for (var i = 0; i < mCount; i++) {
                        var len = reader.ReadInt32();
                        var list = new List<Instruction>();
                        for (var j = 0; j < len; j++) {
                            var code = (OpCode)reader.ReadByte();
                            list.Add(Map.Lookup(code).Deserialize(reader));
                        }

                        _methods.Add(list);
                    }
                }
            }
        }

        readonly Dictionary<string, Assembly> _references;
        readonly List<List<Instruction>> _methods;

        internal Assembly CurrentAssembly { get; set; }

        internal Assembly GetReference(short index) {
            var pair = _references.ElementAt(index);

            if (pair.Value == null)
                _references[pair.Key] = AppDomain.CurrentDomain.Load(new AssemblyName(pair.Key));

            return _references[pair.Key];
        }

        internal List<Instruction> GetMethod(int index) =>
            _methods[index];
    }
}
