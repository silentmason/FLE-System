using System;
using System.Collections.Generic;

namespace Ferneon.FLE.FLEN
{
    public static class FLENNodeRegistry
    {
        private static Dictionary<string, Type> _nodes = new Dictionary<string, Type>();

        public static void Register<T>() where T : FLENNode
        {
            var type = typeof(T);
            _nodes[type.Name] = type;
        }

        public static IEnumerable<Type> GetAllNodes()
        {
            return _nodes.Values;
        }
    }
}
