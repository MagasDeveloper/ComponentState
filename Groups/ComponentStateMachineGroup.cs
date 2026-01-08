using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;
using UnityEngine;

namespace Mahas.ComponentState
{
    public class ComponentStateMachineGroup<TId, TComponent, TState> : MonoBehaviour where TComponent : Component
    {
        [Header("Main")]
        [SerializeField] private TId _defaultStateId;
        
        [Header("Events")] 
        [Space(5)]
        [SerializeField] private UnityEvent _onChange;
        
        //Private
        private readonly HashSet<ComponentStateMachine<TId, TComponent, TState>> _stateMachines = new();
        
        //Events
        public readonly UnityEvent<TId> OnChangeState = new();
        public UnityEvent OnChange => _onChange;
        
        //Public Properties
        public TId CurrentStateId { get; private set; }

        private void Awake()
        {
            Rescan();
        }

        private IEnumerator Start()
        {
            yield return null;
            SetState(_defaultStateId);
        }
        
        public void SetState(TId id)
        {
            if (IsAlreadyActive(id))
                return;
            
            CurrentStateId = id;
            foreach(var stateMachine in _stateMachines)
            {
                stateMachine.SetState(id);
            }
            
            OnChangeState?.Invoke(id);
            OnChange?.Invoke();
        }

        public void Rescan()
        {
            _stateMachines.Clear();

            var machines = GetComponentsInChildren<ComponentStateMachine<TId, TComponent, TState>>(true);
            foreach (var machine in machines)
            {
                if (machine.IgnoreGrouping)
                    continue;
                
                _stateMachines.Add(machine);
            }
        }
        
        private bool IsAlreadyActive(TId id)
        {
            return EqualityComparer<TId>.Default.Equals(CurrentStateId, id);
        }
        
    }
}