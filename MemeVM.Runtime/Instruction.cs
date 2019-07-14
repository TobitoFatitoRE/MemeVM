namespace MemeVM.Runtime {
    struct Instruction {
        internal Instruction(OpCode code, object op = null) {
            Code = code;
            Operand = op;
        }

        internal OpCode Code;
        internal object Operand;
    }
}
