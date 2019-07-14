namespace MemeVM.Translation.Helpers {
    struct Offset {
        internal Offset(int start, int val) {
            Starts = start;
            Value = val;
        }

        internal int Starts;
        internal int Value;
    }
}
