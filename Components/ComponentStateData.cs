using System;

namespace Mahas.ComponentState
{
    [Serializable]
    public struct ComponentStateData<TId, TState>
    {
        public TId Id;
        public TState State;
    }
}