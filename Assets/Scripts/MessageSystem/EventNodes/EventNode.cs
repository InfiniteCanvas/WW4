using UnityEngine;

namespace WW4.EventSystem
{
    public abstract class EventNode : MonoBehaviour
    {
        [SerializeField]private EventNode _root;
        public EventNode Root => _root ?? (_root = _previousNode ? _previousNode.Root : this);

        [SerializeField]private EventNode _previousNode;
        public EventNode PreviousNode => _previousNode;        
        public EventNode NextNode => GetNext();
        [SerializeField] private bool _isActive;
        public bool IsActive => _isActive;
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

        public bool HasNext => GetNext() != null;
        public bool HasPrevious => _previousNode != null;

        public delegate void ActivationEvent();
        public delegate void DeactivationEvent();

        public event ActivationEvent OnActivation;
        public event DeactivationEvent OnDeactivation;

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
            _isActive = active;

            if (active)
                OnActivation?.Invoke();
            else
                OnDeactivation?.Invoke();

            return _isActive;
        }

        protected abstract EventNode GetNext();
    }
}