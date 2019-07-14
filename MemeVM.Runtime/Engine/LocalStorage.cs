using System;

namespace MemeVM.Runtime.Engine {
    class LocalStorage {
        object[] _locals;

        internal LocalStorage() =>
            _locals = new object[10];

        ~LocalStorage() {
            Array.Clear(_locals, 0, _locals.Length);
            _locals = null;
        }

        internal object Get(short index) =>
            _locals[index];

        internal void Set(short index, object val) {
            if (index >= _locals.Length) {
                var arr = new object[2 * _locals.Length];
                Array.Copy(_locals, 0, arr, 0, _locals.Length);
                _locals = arr;
            }

            _locals[index] = val;
        }
    }
}
