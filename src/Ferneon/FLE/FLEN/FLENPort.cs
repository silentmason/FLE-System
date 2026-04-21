using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ferneon.FLE.FLEN
{
    public enum FLENPortDirection { Input, Output }
    public enum FLENPortType { Flow, Number, Boolean, String, Vector, Any }

    public class FLENPort
    {
        public FLENNode Node;
        public string Name;
        public FLENPortType Type;
        public FLENPortDirection Direction;

        // Connections (only for output ports)
        public List<FLENPort> Connections = new List<FLENPort>();

        public FLENPort(FLENNode node, string name, FLENPortType type, FLENPortDirection direction)
        {
            Node = node;
            Name = name;
            Type = type;
            Direction = direction;
        }
    }
}
