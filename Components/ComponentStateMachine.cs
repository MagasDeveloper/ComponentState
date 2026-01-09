using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace Mahas.ComponentState
{
    public abstract class ComponentStateMachine<TId, TComponent, TState> : MonoBehaviour where TComponent : Component
    {
        [Header("Main")]
        [SerializeField] private TComponent _component;
        [SerializeField] private TId _defaultStateId;
        [SerializeField] private bool _ignoreGrouping;
        
        [Header("States")]
        [SerializeField] private ComponentStateData<TId, TState>[] states;

        [Header("Events")] 
        [Space(5)]
        [SerializeField] private UnityEvent _onChange;
        
        //Private
        private readonly Dictionary<TId, TState> _statesMap = new();
        protected TComponent Component => _component;
        
        //Events
        public readonly UnityEvent<TState> OnChangeState = new();
        public readonly UnityEvent<TId> OnChangeId = new();
        public UnityEvent OnChange => _onChange;

        //Public Properties
        public bool IgnoreGrouping => _ignoreGrouping;
        public TId CurrentStateId { get; private set; }

        private void OnValidate()
        {
            _component ??= GetComponent<TComponent>();
        }

        private void OnDestroy()
        {
            OnChangeState?.RemoveAllListeners();
            OnChange?.RemoveAllListeners();
        }

        private void Awake()
        {
            if (!HasRequiredComponent())
            {
                LogComponentNotFound();
                return;
            }
            
            InitializeStates();
            SetState(_defaultStateId);
        }

        public void SetState(TId id)
        {
            if (IsAlreadyActive(id))
                return;
            
            CurrentStateId = id;

            if (!_statesMap.TryGetValue(id, out TState state))
            {
                LogStateNotFound(id);
                return;
            }
            
            ApplyState(state);
            
            OnChangeState?.Invoke(state);
            OnChangeId?.Invoke(id);
            _onChange?.Invoke();
        }
        
        public void OverrideStateValue(TId id, TState state)
        {
            _statesMap[id] = state;
            if (IsAlreadyActive(id))
            {
                ApplyState(state);
                OnChangeState?.Invoke(state);
                OnChangeId?.Invoke(id);
                _onChange?.Invoke();
            }
        }

        protected abstract void ApplyState(TState state);

        private bool IsAlreadyActive(TId id)
        {
            return EqualityComparer<TId>.Default.Equals(CurrentStateId, id);
        }

        private bool HasRequiredComponent()
        {
            if (_component == null)
            {
                if (gameObject.TryGetComponent(out TComponent component))
                {
                    _component = component;
                }
            }
            return _component != null;
        }

        private void InitializeStates()
        {
            foreach (var stateData in states)
            {
                _statesMap[stateData.Id] = stateData.State;
            }
        }
        
        private void LogComponentNotFound()
        {
            Debug.LogError($"Component of type {typeof(TComponent)} is not found", this);
        }

        private void LogStateNotFound(TId id)
        {
            Debug.LogError($"State with id {id} is not found", this);
        }

    }
}
