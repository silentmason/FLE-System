using System.Collections.Generic;
using Ferneon.FLE.FLES;

namespace Ferneon.FLE.FLER
{
    /// <summary>
    /// Runtime instance of a FLESProgram attached to an entity.
    /// </summary>
    public class FLESProgramInstance
    {
        public FLESProgram Program { get; private set; }
        public object[] Variables { get; private set; }
        public Stack<object> Stack { get; private set; }
        public int InstructionPointer { get; set; }
        public bool IsRunning { get; set; }
        public bool Enabled { get; set; } = true;

        public int EntityId { get; private set; }

        public FLESProgramInstance(FLESProgram program, int entityId)
        {
            Program = program;
            Variables = new object[program.VariableTable.Count];
            Stack = new Stack<object>();
            InstructionPointer = 0;
            IsRunning = false;
            EntityId = entityId;
        }

        public void ResetForEvent(int entryPoint)
        {
            InstructionPointer = entryPoint;
            Stack.Clear();
            IsRunning = true;
        }
    }
}
