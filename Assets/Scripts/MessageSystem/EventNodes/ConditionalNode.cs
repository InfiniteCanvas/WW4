using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WW4.EventSystem
{
    public class ConditionalNode : EventNode
    {
        [SerializeField] private Transform[] _conditionalNodeElements;
        public Transform[] ConditionalNodeElements => _conditionalNodeElements;
        public MoveDirection MoveDirection;

        private Coroutine _checkConditions;
        private IConditionalNodeElement[] _elements;

        private void Awake()
        {            
            OnActivation += ()=> _checkConditions = StartCoroutine(CheckConditions());
            OnDeactivation += () => StopCoroutine(_checkConditions);
        }

        private IEnumerator CheckConditions()
        {
            for(;;)
            {
                if(ConditionsFulfilled())
                    OnConditionChange(this, EventArgs.Empty);
                yield return null;
            }
        }

        private void OnConditionChange(Object caller, EventArgs args)
        {
            if (!Active) return;

            if (!ConditionsFulfilled()) return;

            if(MoveDirection == MoveDirection.Next)
                MessageSystem.NodeTraverserEventHandler.Invoke(this, new NodeTraverserEventArgs());

            if(MoveDirection == MoveDirection.Previous)
                MessageSystem.NodeTraverserEventHandler.Invoke(this, new NodeTraverserEventArgs(MoveDirection.Previous));
        }

        private bool ConditionsFulfilled()
        {
            if (_conditionalNodeElements != null)
            {
                if (_elements == null)
                {
                    _elements = _conditionalNodeElements.Where(x => x.GetComponent<IConditionalNodeElement>() != null)
                        .Select(x => x.GetComponent<IConditionalNodeElement>()).ToArray();
                }

                if (_elements.Length < 1) return false;

                return _elements.Aggregate(true, (current, element) => current && element.ConditionFulfilled());
            }
            else
                return false;
        }


    }

    public enum MoveDirection
    {
        Next,
        Previous
    }
}