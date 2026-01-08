using UnityEngine;
using System;

namespace Mahas.ComponentState
{
    
    [Serializable]
    public struct RectComponentState
    {
        public Vector2 AnchoredPosition;
        public Vector2 SizeDelta;
    }
    
    [RequireComponent(typeof(RectTransform))]
    public class RectComponentStateMachine : ComponentStateMachine<int, RectTransform, RectComponentState>
    {
        protected override void ApplyState(RectComponentState state)
        {
            Component.anchoredPosition = state.AnchoredPosition;
            Component.sizeDelta = state.SizeDelta;
        }
    }
}