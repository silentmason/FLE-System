using System.Collections.Generic;
using UnityEngine;

namespace Ferneon.FLE.FLEN
{
    public abstract class FLENNode
    {
        // Unique ID for this node instance in the graph
        public string Guid;

        // Position in the editor canvas
        public Vector2 Position;

        // Node metadata
        public abstract string Name { get; }
        public abstract string Category { get; }
        public virtual Color NodeColor => new Color(0.25f, 0.25f, 0.25f);

        // Ports
        public List<FLENPort> Inputs = new List<FLENPort>();
        public List<FLENPort> Outputs = new List<FLENPort>();

        // Called by the compiler to generate FLES or bytecode
        public abstract void Compile(FLENCompiler compiler);

        // Utility: Add an input port
        protected FLENPort AddInput(string name, FLENPortType type)
        {
            var port = new FLENPort(this, name, type, FLENPortDirection.Input);
            Inputs.Add(port);
            return port;
        }

        // Utility: Add an output port
        protected FLENPort AddOutput(string name, FLENPortType type)
        {
            var port = new FLENPort(this, name, type, FLENPortDirection.Output);
            Outputs.Add(port);
            return port;
        }
    }
}
