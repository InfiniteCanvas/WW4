using UnityEngine;

namespace WW4.EventSystem
{
    public class NodeTraverser : MonoBehaviour
    {
        [SerializeField] private int _eventSystemID;
        public int EventSystemID => _eventSystemID;

        [SerializeField] private EventNode _currentNode;
        public EventNode CurrentNode => _currentNode;

        private void Start()
        {
            MessageSystem.NodeTraverserEventHandler.AddListener(OnEventNodeTraverserEvent);
        }

        private void OnEventNodeTraverserEvent(EventNode node, NodeTraverserEventArgs args)
        {
            if (node.EventSystemID != _eventSystemID) return;

            if (args.MoveDirection == MoveDirection.Next)
            {
                if (_currentNode.HasNext)
                    MoveToNextNode();
                else
                    Debug.Log($"Reached the end of EventTree {_eventSystemID}.");
            }

            if(args.MoveDirection == MoveDirection.Previous)
            {
                if(_currentNode.HasPrevious)
                    MoveToPreviousNode();
                else
                    Debug.Log($"Reached the beginning of EventTree {_eventSystemID}.");
            }
        }

        private void MoveToNextNode()
        {
            _currentNode.SetActive(false);
            _currentNode = _currentNode.NextNode;
            _currentNode.SetActive(true);
        }

        private void MoveToPreviousNode()
        {
            _currentNode.SetActive(false);
            _currentNode = _currentNode.PreviousNode;
            _currentNode.SetActive(true);
        }
    }
}