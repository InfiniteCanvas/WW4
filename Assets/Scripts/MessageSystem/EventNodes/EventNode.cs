using UnityEngine;

namespace WW4.EventSystem
{
    public abstract class EventNode : MonoBehaviour
    {
        [SerializeField]private EventNode _root;
        public EventNode Root => _root ?? (_root = _previousNode ? _previousNode.Root : this);

        [SerializeField]private EventNode _previousNode;
        public EventNode PreviousNode => _previousNode;
        [SerializeField]private EventNode _nextNode;
        public EventNode NextNode => _nextNode;
        [SerializeField] private bool _active;
        public bool Active => _active;
        [SerializeField] private int _eventSystemID;

        public int EventSystemID
        {
            get
            {
                if (_eventSystemID == 0 && HasPrevious)
                    return _eventSystemID = _previousNode.EventSystemID;
                else
                    return _eventSystemID;
            }
        }

        public bool HasNext => _nextNode != null;
        public bool HasPrevious => _previousNode != null;

        public delegate void ActivationEvent();
        public delegate void DeactivationEvent();

        public event ActivationEvent OnActivation;
        public event DeactivationEvent OnDeactivation;

        /// <summary>
        /// Returns true if next node is set to a value, false if node is set to null.
        /// </summary>
        /// <param name="node">Next EventNode.</param>
        /// <returns></returns>
        public bool SetNextNode(EventNode node)
        {
            if (node)
            {
                _nextNode = node;
                return true;
            }
            else
            {
                _nextNode = null;
                return false;
            }
        }

        /// <summary>
        /// Returns true if previous node is set to a value, false if node is set to null.
        /// </summary>
        /// <param name="node">Previous EventNode.</param>
        /// <returns></returns>
        public bool SetPreviousNode(EventNode node)
        {
            if (node)
            {
                _previousNode = node;
                return true;
            }
            else
            {
                _previousNode = null;
                return false;
            }
        }

        public virtual bool SetActive(bool active)
        {
            _active = active;

            if (active)
                OnActivation?.Invoke();
            else
                OnDeactivation?.Invoke();

            return _active;
        }
    }
}