using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW4.EventSystem
{
    public class ActionNode : EventNode
    {
        [SerializeField] private EventNode _nextNode;

        private void Awake()
        {
            OnActivation += Action;
        }

        private void Action()
        {
            Debug.Log($"Matched all birds!\nEnd reached. Root is {Root.name}. EventSystemID is {EventSystemID}.");
        }

        protected override EventNode GetNext()
        {
            return _nextNode;
        }
    }
}