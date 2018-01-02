using UnityEngine;
using WW4.EventSystem;

namespace WW4.Tutorial
{
    public class TutorialNode : MonoBehaviour, IConditionalNodeElement
    {
        private TutorialPage _tutorialPage;

        private void Start()
        {
            _tutorialPage = GetComponent<TutorialPage>();
        }

        public bool ConditionFulfilled()
        {
            return _tutorialPage?.Matched ?? false;
        }
    }
}