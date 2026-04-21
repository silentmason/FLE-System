using System;
using System.Collections.Generic;
using Ferneon.FLE.FLER;

namespace Ferneon.FLE.FLEA
{
    public delegate void FLEApiHandler(FLESProgramInstance instance, int argCount);

    /// <summary>
    /// Registry of safe engine APIs callable from FLES scripts.
    /// </summary>
    public class FLEAApiRegistry
    {
        private readonly Dictionary<int, FLEApiHandler> _apis = new();

        public void Register(int apiId, FLEApiHandler handler)
        {
            _apis[apiId] = handler;
        }

        public bool TryGet(int apiId, out FLEApiHandler handler)
        {
            return _apis.TryGetValue(apiId, out handler);
        }
    }
}
