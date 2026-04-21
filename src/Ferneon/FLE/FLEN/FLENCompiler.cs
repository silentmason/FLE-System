using System;
using System.Collections.Generic;
using UnityEngine;

// FLEB: Instruction, OpCode
using Ferneon.FLE.FLEB;
// FLES: FLESProgram, FLESVariableTable, FLESEventTable, FLESEventType
using Ferneon.FLE.FLES;

namespace Ferneon.FLE.FLEN
{
    public class FLENCompiler
    {
        // Output
        private readonly List<Instruction> instructions = new();
        private readonly List<object> constants = new();
        private readonly Dictionary<string, int> variableTable = new();
        private readonly Dictionary<FLESEventType, int> eventEntryPoints = new();

        // Graph
        private readonly FLENGraph graph;

        public FLENCompiler(FLENGraph graph)
        {
            this.graph = graph;
        }

        // ------------------------------------------------------------
        // PUBLIC API
        // ------------------------------------------------------------

        public FLESProgram Compile()
        {
            // Compile each event entry node
            foreach (var node in graph.Nodes)
            {
                if (node is FLENEventNode eventNode)
                    CompileEvent(eventNode);
            }

            return new FLESProgram(
                graph.Name,
                instructions.ToArray(),
                constants.ToArray(),
                BuildVariableTable(),
                BuildEventTable()
            );
        }

        // ------------------------------------------------------------
        // EVENT COMPILATION
        // ------------------------------------------------------------

        private void CompileEvent(FLENEventNode eventNode)
        {
            // Mark entry point
            eventEntryPoints[eventNode.EventType] = instructions.Count;

            // Compile the flow starting from this node
            CompileFlow(eventNode);
        }

        // ------------------------------------------------------------
        // FLOW COMPILATION
        // ------------------------------------------------------------

        private void CompileFlow(FLENNode node)
        {
            // Compile this node
            node.Compile(this);

            // Follow the flow output
            foreach (var port in node.Outputs)
            {
                if (port.Type == FLENPortType.Flow)
                {
                    foreach (var connection in port.Connections)
                    {
                        CompileFlow(connection.Node);
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // EXPRESSION COMPILATION
        // ------------------------------------------------------------

        public void CompilePort(FLENPort port)
        {
            if (port == null)
                throw new Exception("Port is null");

            if (port.Connections.Count == 0)
                throw new Exception($"Port '{port.Name}' has no input");

            // Only one input allowed for expressions
            var source = port.Connections[0].Node;
            source.Compile(this);
        }

        // ------------------------------------------------------------
        // BYTECODE EMISSION
        // ------------------------------------------------------------

        public void Emit(Instruction instruction)
        {
            instructions.Add(instruction);
        }

        public void EmitCallApi(int apiId, int argCount)
        {
            instructions.Add(new Instruction(OpCode.CallApi, apiId, argCount));
        }

        public void EmitReturn()
        {
            instructions.Add(new Instruction(OpCode.Return));
        }

        public void PushConst(object value)
        {
            int index = AddConstant(value);
            instructions.Add(new Instruction(OpCode.PushConst, index));
        }

        public void PushVar(string name)
        {
            int index = GetVariableIndex(name);
            instructions.Add(new Instruction(OpCode.LoadVar, index));
        }

        public void StoreVar(string name)
        {
            int index = GetVariableIndex(name);
            instructions.Add(new Instruction(OpCode.StoreVar, index));
        }

        // ------------------------------------------------------------
        // CONSTANTS
        // ------------------------------------------------------------

        private int AddConstant(object value)
        {
            constants.Add(value);
            return constants.Count - 1;
        }

        // ------------------------------------------------------------
        // VARIABLES
        // ------------------------------------------------------------

        private int GetVariableIndex(string name)
        {
            if (!variableTable.ContainsKey(name))
                variableTable[name] = variableTable.Count;

            return variableTable[name];
        }

        private FLESVariableTable BuildVariableTable()
        {
            var table = new FLESVariableTable();

            foreach (var kvp in variableTable)
            {
                // For now, everything is typed as Any; you can refine this later.
                table.Add(kvp.Key, FLESVariableType.Any);
            }

            return table;
        }

        // ------------------------------------------------------------
        // EVENTS
        // ------------------------------------------------------------

        private FLESEventTable BuildEventTable()
        {
            var table = new FLESEventTable();

            foreach (var kvp in eventEntryPoints)
                table.SetEntryPoint(kvp.Key, kvp.Value);

            return table;
        }
    }
}
