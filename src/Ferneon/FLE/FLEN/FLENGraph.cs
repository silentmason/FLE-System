using System.Collections.Generic;

namespace Ferneon.FLE.FLEN
{
    /// <summary>
    /// In-memory representation of a visual script graph.
    /// This is what FLENCompiler consumes.
    /// </summary>
    public class FLENGraph
    {
        public string Name;

        // All nodes in this graph
        public List<FLENNode> Nodes = new List<FLENNode>();

        public FLENGraph(string name)
        {
            Name = name;
        }

        public void AddNode(FLENNode node)
        {
            if (!Nodes.Contains(node))
                Nodes.Add(node);
        }
    }
}
