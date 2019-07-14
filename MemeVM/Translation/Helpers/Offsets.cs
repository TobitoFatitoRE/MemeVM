using System.Collections.Generic;
using System.Linq;

namespace MemeVM.Translation.Helpers {
    class Offsets {
        internal Offsets() =>
            _offsets = new List<Offset>();

        readonly List<Offset> _offsets;

        internal void Add(int index, int offset) =>
            _offsets.Add(new Offset(index, offset));

        internal int Get(int index) =>
            _offsets.Where(o => o.Starts < index).Sum(off => off.Value) - 1 + index;
    }
}
