namespace MemeVM.Translation {
    struct VMInstruction {
        internal VMInstruction(VMOpCode code, object op = null) {
            OpCode = code;
            Operand = op;
        }

        internal VMOpCode OpCode;
        internal object Operand;
    }
}
