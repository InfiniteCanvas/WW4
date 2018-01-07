using UnityEngine;
using WW4.Utility;

namespace WW4.EventSystem
{
    public class InteractableNode : EventNode, IInteractable
    {
        [SerializeField] private EventNode _nextNode;

        public void Interact(GameObject heldObject = null)
        {
            if (IsActive)
                MessageSystem.NodeTraverserEventHandler.Invoke(this, new NodeTraverserEventArgs(NextNode));
        }

        protected override EventNode GetNext()
        {
            return _nextNode;
        }
    }
}