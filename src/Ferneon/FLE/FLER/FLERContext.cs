using System.Collections.Generic;
using Ferneon.FLE.FLES;

namespace Ferneon.FLE.FLER
{
    /// <summary>
    /// Manages all running FLESProgramInstances in a world/session.
    /// </summary>
    public class FLERContext
    {
        private readonly List<FLESProgramInstance> _instances = new();
        private readonly FLERInterpreter _interpreter;

        public FLERContext(FLERInterpreter interpreter)
        {
            _interpreter = interpreter;
        }

        public FLESProgramInstance AddInstance(FLESProgram program, int entityId)
        {
            var inst = new FLESProgramInstance(program, entityId);
            _instances.Add(inst);
            return inst;
        }

        public void RemoveInstance(FLESProgramInstance instance)
        {
            _instances.Remove(instance);
        }

        public void FireEvent(FLESEventType type, int entityId)
        {
            foreach (var inst in _instances)
            {
                if (!inst.Enabled || inst.EntityId != entityId)
                    continue;

                if (inst.Program.EventTable.TryGetEntryPoint(type, out int ip))
                {
                    inst.ResetForEvent(ip);
                    _interpreter.Run(inst);
                }
            }
        }

        public void FireGlobalUpdate()
        {
            foreach (var inst in _instances)
            {
                if (!inst.Enabled)
                    continue;

                if (inst.Program.EventTable.TryGetEntryPoint(FLESEventType.OnUpdate, out int ip))
                {
                    inst.ResetForEvent(ip);
                    _interpreter.Run(inst);
                }
            }
        }
    }
}
