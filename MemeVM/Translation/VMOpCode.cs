namespace MemeVM.Translation {
    enum VMOpCode : byte {
        Int32,
        Int64,
        Float,
        Double,
        String,
        Null,

        Add,
        Sub,
        Mul,
        Div,
        Rem,

        Dup,
        Pop,

        Jmp,
        Jt,
        Jf,
        Je,
        Jne,
        Jge,
        Jgt,
        Jle,
        Jlt,

        Cmp,
        Cgt,
        Clt,

        Newarr,

        Ldarg,
        Ldloc,
        Ldfld,
        Ldelem,

        Starg,
        Stloc,
        Stfld,
        Stelem,

        Call,

        Ret,

        //DONT CHANGE
        UNUSED
    }
}
