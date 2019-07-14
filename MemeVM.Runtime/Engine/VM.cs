using System;
using MemeVM.Runtime.Helpers;

namespace MemeVM.Runtime.Engine {
    class VM {
        readonly Instruction[] _instructions;

        internal int Ip;
        internal VMState State;
        internal readonly Stack Stack;
        internal readonly LocalStorage Locals;
        internal readonly Body VMBody;
        internal object[] Parameters;

        internal VM(Instruction[] program, Body body, object[] parameters) {
            _instructions = program;

            Ip = 0;
            State = VMState.Next;
            Stack = new Stack();
            Locals = new LocalStorage();
            VMBody = body;
            Parameters = parameters;
        }

        internal object Run() {
            for (;State == VMState.Next; ++Ip)
                Map.Lookup(_instructions[Ip].Code).Handle(this, VMBody, _instructions[Ip]);

            if (State == VMState.Return)
                return Stack.Pop();

            throw (Exception)Stack.Pop();
        }
    }
}
