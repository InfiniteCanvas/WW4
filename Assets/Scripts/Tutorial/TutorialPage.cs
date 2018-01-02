using UnityEngine;
using WW4.EventSystem;
using WW4.Utility;

namespace WW4.Tutorial
{
    public class TutorialPage : MonoBehaviour, IInteractable, IConditionalNodeElement
    {
        public TutorialBird.BirdName Name;

        public bool Matched { get; private set; }

        public void Interact(GameObject heldObject = null)
        {
            var tb = heldObject?.GetComponent<TutorialBird>();
            if (tb != null)
                if (tb.Name == Name)
                    OnMatch(heldObject);
                else
                    OnMismatch();
        }

        private void OnMatch(GameObject go)
        {
            print("You matched the right bird to the right page!");
            Matched = true;
            Destroy(go);
        }

        private void OnMismatch()
        {
            print("Wrong birdy.");
        }

        public bool ConditionFulfilled()
        {
            return Matched;
        }
    }
}