using UnityEngine;
using Ferneon.FLE.FLES;

namespace Ferneon.FLE.FLEN
{
    public class FLENEventNode : FLENNode
    {
        public override string Name => EventType.ToString();
        public override string Category => "Events";
        public override Color NodeColor => new Color(0.4f, 0.8f, 0.4f);

        public FLESEventType EventType;

        private FLENPort flowOutput;

        public FLENEventNode(FLESEventType eventType)
        {
            EventType = eventType;
            flowOutput = AddOutput("Flow", FLENPortType.Flow);
        }

        public override void Compile(FLENCompiler compiler)
        {
            // Event nodes don't emit bytecode directly.
        }

        public FLENPort GetFlowOutput() => flowOutput;
    }
}
