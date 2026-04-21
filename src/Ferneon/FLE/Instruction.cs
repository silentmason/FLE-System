using System;

namespace Ferneon.FLE.FLEB
{
    public enum OpCode : byte
    {
        PushConst,
        LoadVar,
        StoreVar,
        Add,
        Sub,
        Mul,
        Div,
        Eq,
        Neq,
        Lt,
        Gt,
        Lte,
        Gte,
        And,
        Or,
        Not,
        Jump,
        JumpIfFalse,
        CallApi,
        Return
    }

    public struct Instruction
    {
        public OpCode OpCode;
        public int A;
        public int B;

        public Instruction(OpCode op, int a = 0, int b = 0)
        {
            OpCode = op;
            A = a;
            B = b;
        }
    }
}
