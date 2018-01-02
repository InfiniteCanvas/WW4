using UnityEngine;

namespace WW4.EventSystem
{
    public class NodeTraverser : MonoBehaviour
    {
        [SerializeField] private int _eventSystemID;
        public int EventSystemID => _eventSystemID;

        [SerializeField]private EventNode _currentNode;
        public EventNode CurrentNode => _currentNode;

        public EventNode StartingNode;

        private void Start()
        {
            MessageSystem.NodeTraverserEventHandler.AddListener(OnEventNodeTraverserEvent);
            StartingNode.SetActive(true);
            _currentNode = StartingNode;
        }

        private void OnEventNodeTraverserEvent(EventNode node, NodeTraverserEventArgs args)
        {
            if (node.EventSystemID != _eventSystemID) return;

            if (args.HasTargetNode)
                MoveToNode(args.TargetNode);
            else
                Debug.Log($"Reached the end of EventTree {_eventSystemID}.");
        }

        private void MoveToNextNode()
        {
            _currentNode.SetActive(false);
            _currentNode = _currentNode.NextNode;
            _currentNode.SetActive(true);
        }

        private void MoveToNode(EventNode targetNode)
        {
            _currentNode.SetActive(false);
            _currentNode = targetNode;
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