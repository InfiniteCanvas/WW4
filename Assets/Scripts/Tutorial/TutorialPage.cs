using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW4.Utility;

namespace WW4.Entities
{
    public class TutorialPage : MonoBehaviour, IInteractable
    {
        public TutorialBird.BirdName Name;

        public void Interact(GameObject heldObject = null)
        {
            var tb = heldObject?.GetComponent<TutorialBird>();
            if (tb != null)
            {
                if (tb.Name == Name)
                    print("You matched the right bird to the right page!");
            }
        }
    }
}