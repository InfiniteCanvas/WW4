using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WW4.EventSystem
{
    public class ConditionalNode : EventNode
    {
        private Coroutine _checkConditions;
        [SerializeField] private Transform[] _conditionalNodeElements;
        public Transform[] ConditionalNodeElements => _conditionalNodeElements;
        private IConditionalNodeElement[] _elements;

        [SerializeField] private EventNode _nextNode;

        private void Awake()
        {
            OnActivation.AddListener(() => _checkConditions = StartCoroutine(CheckConditions()));
            OnDeactivation.AddListener(() => StopCoroutine(_checkConditions));
        }

        private IEnumerator CheckConditions()
        {
            for (;;)
            {
                if (ConditionsFulfilled())
                    OnConditionsFulfilled(this, EventArgs.Empty);
                //foreach (var element in _conditionalNodeElements)
                //{
                //    print($"{element.name} condition fulfilled: {element.GetComponent<IConditionalNodeElement>().ConditionFulfilled()}");
                //}
                yield return null;
            }
        }

        private void OnConditionsFulfilled(Object caller, EventArgs args)
        {
            if (!IsActive) return;

            if (!ConditionsFulfilled()) return;

            MessageSystem.NodeTraverserEventHandler.Invoke(this, new NodeTraverserEventArgs(NextNode));
        }

        private bool ConditionsFulfilled()
        {
            if (_conditionalNodeElements == null) return false;

            if (_elements == null)
                _elements = _conditionalNodeElements.Select(x => x.GetComponent<IConditionalNodeElement>())
                    .Where(x => x != null).ToArray();

            if (_elements.Length < 1) return false;

            return _elements.Aggregate(true, (current, element) => current && element.ConditionFulfilled());
        }

        protected override EventNode GetNext()
        {
            return _nextNode;
        }
    }
}