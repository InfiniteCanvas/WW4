using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW4.Utility;

namespace WW4.EventSystem
{
    public class InteractableNode : EventNode, IInteractable
    {
        public void Interact(GameObject heldObject = null)
        {
            if(Active)
                MessageSystem.NodeTraverserEventHandler.Invoke(this, new NodeTraverserEventArgs());
        }
    }
}