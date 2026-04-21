using System;
using UnityEngine;
using Ferneon.FLE.FLEA;
using Ferneon.FLE.FLEB;

namespace Ferneon.FLE.FLER
{
    /// <summary>
    /// Executes FLEB bytecode for a single FLESProgramInstance.
    /// </summary>
    public class FLERInterpreter
    {
        private readonly FLEAApiRegistry _apiRegistry;

        public FLERInterpreter(FLEAApiRegistry apiRegistry)
        {
            _apiRegistry = apiRegistry;
        }

        public void Run(FLESProgramInstance instance)
        {
            var program = instance.Program;
            var code = program.Bytecode;

            instance.IsRunning = true;

            while (instance.IsRunning &&
                   instance.InstructionPointer >= 0 &&
                   instance.InstructionPointer < code.Length)
            {
                var instr = code[instance.InstructionPointer];

                switch (instr.OpCode)
                {
                    // ------------------------------------------------------
                    // CONSTANTS & VARIABLES
                    // ------------------------------------------------------
                    case OpCode.PushConst:
                        instance.Stack.Push(program.Constants[instr.A]);
                        instance.InstructionPointer++;
                        break;

                    case OpCode.LoadVar:
                        instance.Stack.Push(instance.Variables[instr.A]);
                        instance.InstructionPointer++;
                        break;

                    case OpCode.StoreVar:
                        instance.Variables[instr.A] = instance.Stack.Pop();
                        instance.InstructionPointer++;
                        break;

                    // ------------------------------------------------------
                    // MATH (No dynamic!)
                    // ------------------------------------------------------
                    case OpCode.Add:
                    {
                        float b = Convert.ToSingle(instance.Stack.Pop());
                        float a = Convert.ToSingle(instance.Stack.Pop());
                        instance.Stack.Push(a + b);
                        instance.InstructionPointer++;
                        break;
                    }

                    case OpCode.Sub:
                    {
                        float b = Convert.ToSingle(instance.Stack.Pop());
                        float a = Convert.ToSingle(instance.Stack.Pop());
                        instance.Stack.Push(a - b);
                        instance.InstructionPointer++;
                        break;
                    }

                    case OpCode.Mul:
                    {
                        float b = Convert.ToSingle(instance.Stack.Pop());
                        float a = Convert.ToSingle(instance.Stack.Pop());
                        instance.Stack.Push(a * b);
                        instance.InstructionPointer++;
                        break;
                    }

                    case OpCode.Div:
                    {
                        float b = Convert.ToSingle(instance.Stack.Pop());
                        float a = Convert.ToSingle(instance.Stack.Pop());
                        instance.Stack.Push(a / b);
                        instance.InstructionPointer++;
                        break;
                    }

                    // ------------------------------------------------------
                    // COMPARISONS
                    // ------------------------------------------------------
                    case OpCode.Eq:
                    {
                        var b = instance.Stack.Pop();
                        var a = instance.Stack.Pop();
                        instance.Stack.Push(Equals(a, b));
                        instance.InstructionPointer++;
                        break;
                    }

                    case OpCode.Neq:
                    {
                        var b = instance.Stack.Pop();
                        var a = instance.Stack.Pop();
                        instance.Stack.Push(!Equals(a, b));
                        instance.InstructionPointer++;
                        break;
                    }

                    case OpCode.Lt:
                    {
                        float b = Convert.ToSingle(instance.Stack.Pop());
                        float a = Convert.ToSingle(instance.Stack.Pop());
                        instance.Stack.Push(a < b);
                        instance.InstructionPointer++;
                        break;
                    }

                    case OpCode.Gt:
                    {
                        float b = Convert.ToSingle(instance.Stack.Pop());
                        float a = Convert.ToSingle(instance.Stack.Pop());
                        instance.Stack.Push(a > b);
                        instance.InstructionPointer++;
                        break;
                    }

                    case OpCode.Lte:
                    {
                        float b = Convert.ToSingle(instance.Stack.Pop());
                        float a = Convert.ToSingle(instance.Stack.Pop());
                        instance.Stack.Push(a <= b);
                        instance.InstructionPointer++;
                        break;
                    }

                    case OpCode.Gte:
                    {
                        float b = Convert.ToSingle(instance.Stack.Pop());
                        float a = Convert.ToSingle(instance.Stack.Pop());
                        instance.Stack.Push(a >= b);
                        instance.InstructionPointer++;
                        break;
                    }

                    // ------------------------------------------------------
                    // BOOLEAN LOGIC
                    // ------------------------------------------------------
                    case OpCode.And:
                    {
                        bool b = Convert.ToBoolean(instance.Stack.Pop());
                        bool a = Convert.ToBoolean(instance.Stack.Pop());
                        instance.Stack.Push(a && b);
                        instance.InstructionPointer++;
                        break;
                    }

                    case OpCode.Or:
                    {
                        bool b = Convert.ToBoolean(instance.Stack.Pop());
                        bool a = Convert.ToBoolean(instance.Stack.Pop());
                        instance.Stack.Push(a || b);
                        instance.InstructionPointer++;
                        break;
                    }

                    case OpCode.Not:
                    {
                        bool a = Convert.ToBoolean(instance.Stack.Pop());
                        instance.Stack.Push(!a);
                        instance.InstructionPointer++;
                        break;
                    }

                    // ------------------------------------------------------
                    // FLOW CONTROL
                    // ------------------------------------------------------
                    case OpCode.Jump:
                        instance.InstructionPointer = instr.A;
                        break;

                    case OpCode.JumpIfFalse:
                    {
                        var cond = instance.Stack.Pop();
                        bool isFalse = cond is bool b && !b;

                        instance.InstructionPointer = isFalse
                            ? instr.A
                            : instance.InstructionPointer + 1;
                        break;
                    }

                    // ------------------------------------------------------
                    // API CALLS
                    // ------------------------------------------------------
                    case OpCode.CallApi:
                        if (_apiRegistry.TryGet(instr.A, out var handler))
                        {
                            handler(instance, instr.B);
                        }
                        instance.InstructionPointer++;
                        break;

                    // ------------------------------------------------------
                    // RETURN
                    // ------------------------------------------------------
                    case OpCode.Return:
                        instance.IsRunning = false;
                        break;

                    // ------------------------------------------------------
                    // UNKNOWN OPCODE
                    // ------------------------------------------------------
                    default:
                        instance.IsRunning = false;
                        break;
                }
            }
        }
    }
}
