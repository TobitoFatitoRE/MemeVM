using System;

namespace MemeVM.Runtime.Engine {
    class Stack {
        object[] _array;
        uint _index;

        internal Stack() {
            _array = new object[10];
            _index = 0;
        }

        ~Stack() {
            Array.Clear(_array, 0, _array.Length);
            _array = null;
            _index = 0;
        }

        internal void Push(object val) {
            if (_index == _array.Length) {
                var arr = new object[2 * _array.Length];
                Array.Copy(_array, 0, arr, 0, _index);
                _array = arr;
            }

            _array[_index++] = val;
        }

        internal object Pop() {
            if (_index == 0)
                return new NoMoreStackItem();

            var res = _array[--_index];
            _array[_index] = null;
            return res;
        }
    }
}
