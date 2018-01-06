using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WW4.EventSystem
{
    public class ActionNode : EventNode
    {
        [SerializeField] private EventNode _nextNode;
        public UnityEvent ActionHandler;

        private void Awake()
        {
            OnActivation.AddListener(Action);
        }

        private void Action()
        {
            ActionHandler?.Invoke();
            MessageSystem.NodeTraverserEventHandler.Invoke(this, new NodeTraverserEventArgs(NextNode));
        }

        protected override EventNode GetNext()
        {
            return _nextNode;
        }
    }
}