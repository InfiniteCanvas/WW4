using UnityEngine;

namespace WW4.EventSystem
{
    public class NodeTraverser : MonoBehaviour
    {
        [SerializeField] private EventNode _startingNode;
        public EventNode CurrentNode { get; private set; }

        private void Start()
        {
            MessageSystem.NodeTraverserEventHandler.AddListener(OnEventNodeTraverserEvent);
            _startingNode.SetActive(true);
            CurrentNode = _startingNode;
        }

        private void OnEventNodeTraverserEvent(EventNode node, NodeTraverserEventArgs args)
        {
            if (node.Root != _startingNode.Root) return;

            if (args.HasTargetNode)
                MoveToNode(args.TargetNode);
            else
                Debug.Log($"Reached the end of EventTree with root {_startingNode.Root}.");
        }

        private void MoveToNode(EventNode targetNode)
        {
            CurrentNode.SetActive(false);
            CurrentNode = targetNode;
            CurrentNode.SetActive(true);
        }
    }
}