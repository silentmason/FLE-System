using System;
using System.Collections.Generic;
using Ferneon.FLE.FLEB;

namespace Ferneon.FLE.FLES
{
    /// <summary>
    /// Compiled FLES script: FLEB bytecode + metadata.
    /// </summary>
    public class FLESProgram
    {
        public string ProgramId { get; private set; }
        public Instruction[] Bytecode { get; private set; }
        public object[] Constants { get; private set; }
        public FLESVariableTable VariableTable { get; private set; }
        public FLESEventTable EventTable { get; private set; }
        public string SourceDebugInfo { get; private set; }

        public FLESProgram(
            string programId,
            Instruction[] bytecode,
            object[] constants,
            FLESVariableTable variableTable,
            FLESEventTable eventTable,
            string debugInfo = null)
        {
            ProgramId = programId;
            Bytecode = bytecode;
            Constants = constants;
            VariableTable = variableTable;
            EventTable = eventTable;
            SourceDebugInfo = debugInfo;
        }
    }

    public class FLESVariableTable
    {
        public struct VariableInfo
        {
            public string Name;
            public FLESVariableType Type;
            public int Index;
        }

        private readonly List<VariableInfo> _variables = new();

        public int Count => _variables.Count;

        public void Add(string name, FLESVariableType type)
        {
            _variables.Add(new VariableInfo
            {
                Name = name,
                Type = type,
                Index = _variables.Count
            });
        }

        public VariableInfo Get(int index) => _variables[index];
    }

    public enum FLESVariableType
    {
        Number,
        Boolean,
        String,
        Vector3,
        EntityRef,
        Any
    }

    public class FLESEventTable
    {
        private readonly Dictionary<FLESEventType, int> _entryPoints = new();

        public void SetEntryPoint(FLESEventType type, int instructionIndex)
        {
            _entryPoints[type] = instructionIndex;
        }

        public bool TryGetEntryPoint(FLESEventType type, out int instructionIndex)
        {
            return _entryPoints.TryGetValue(type, out instructionIndex);
        }
    }
}
