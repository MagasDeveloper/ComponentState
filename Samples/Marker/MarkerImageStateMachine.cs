using UnityEngine.UI;
using UnityEngine;
using System;

namespace Mahas.ComponentState
{
    public enum MarkerType
    {
        Notification,
        Important,
    }

    [Serializable]
    public struct MarkerVisualState
    {
        public Sprite MarkerSprite;
        public Color Color;
        public bool UseColor;
    }
    
    [RequireComponent(typeof(Image))]
    public class MarkerImageStateMachine : ComponentStateMachine<MarkerType, Image, MarkerVisualState>
    {
        protected override void ApplyState(MarkerVisualState state)
        {
            Component.sprite = state.MarkerSprite;
            
            Color color = state.UseColor ? state.Color : Color.white;
            Component.color = color;
        }
    }
}