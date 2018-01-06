using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW4.EventSystem
{
    public class ForkingNode : EventNode
    {
        protected override EventNode GetNext()
        {
            Debug.LogWarning($"{nameof(ForkingNode)} has no one 'next node', since it has multiple.");
            throw new System.NotImplementedException();
        }

        [SerializeField] private EventNode[] _nodes;

        private void Awake()
        {
            OnActivation.AddListener(() =>
                {
                    foreach (var eventNode in _nodes)
                    {
                        eventNode.SetActive(true);
                    }
                }
            );

            OnDeactivation.AddListener(() =>
                {
                    foreach (var eventNode in _nodes)
                    {
                        eventNode.SetActive(false);
                    }
                }
            );
        }
    }
}