using UnityEngine;
using WW4.EventSystem;
using WW4.Utility;

namespace WW4.Tutorial
{
    public class TutorialPage : MonoBehaviour, IInteractable, IConditionalNodeElement
    {
        public TutorialBird.BirdName Name;
        public int NeededMatches { get; private set; }
        private int _matched;

        public bool Matched => _matched == NeededMatches;

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
            _matched++;
            Destroy(go.GetComponent<FixedJoint>());
            go.SetActive(false);
        }

        private void OnMismatch()
        {
            print("Wrong birdy.");
        }

        public bool ConditionFulfilled()
        {
            return Matched;
        }

        public void ResetMatched()
        {
            _matched = 0;
            print($"{name} has been reset.");
        }

        public void SetNeededMatches(int matches)
        {
            NeededMatches = matches;
            print($"{name} set needed matches to {NeededMatches}.");
        }
    }
}