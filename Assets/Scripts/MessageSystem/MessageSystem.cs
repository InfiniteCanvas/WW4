using System;
using UnityEngine;
using UnityEngine.Events;
using WW4.Entities;
using WW4.Utility;

namespace WW4.EventSystem
{
    public class MessageSystem : MonoBehaviour, InitializerControlled
    {

        public static BirdHitEvent BirdHitEventHandler { get; private set; }
        public static ReturningToPoolEvent ReturningToPoolEventHandler { get; private set; }
        public static EntityGrabbedEvent EntityGrabbedEventHandler { get; private set; }
        public static NodeTraverserEvent NodeTraverserEventHandler { get; private set; }

        public bool Initialize()
        {
            BirdHitEventHandler = new BirdHitEvent();
            BirdHitEventHandler.AddListener(x=>Debug.Log($"{x.ClipUrl} was hit"));
            ReturningToPoolEventHandler = new ReturningToPoolEvent();
            ReturningToPoolEventHandler.AddListener(x=>Debug.Log($"{x.name} returned to its pool"));
            EntityGrabbedEventHandler = new EntityGrabbedEvent();
            EntityGrabbedEventHandler.AddListener((x,y)=>Debug.Log($"GameObject [{x.name}] of type [{y.GetContractorType().FullName}] was grabbed."));
            NodeTraverserEventHandler = new NodeTraverserEvent();
            NodeTraverserEventHandler.AddListener((x,y)=>Debug.Log($"{nameof(NodeTraverserEvent)} was called with Node: {x.name} in system: {x.EventSystemID}\nArgs: {y.MoveDirection}"));

            return true;
        }

        public string GetClassName()
        {
            return typeof(MessageSystem).FullName;
        }
    }

    public class BirdHitEvent : UnityEvent<Bird>
    {
    }

    public class ReturningToPoolEvent : UnityEvent<GameObject>
    {
    }

    public class EntityGrabbedEvent : UnityEvent<GameObject, IGrabbable>
    {
    }

    public class NodeTraverserEvent : UnityEvent<EventNode, NodeTraverserEventArgs>
    {
    }

    public class NodeTraverserEventArgs : EventArgs
    {
        public NodeTraverserEventArgs(MoveDirection moveDirection = MoveDirection.Next)
        {
            MoveDirection = moveDirection;
        }

        public MoveDirection MoveDirection { get; }
    }
}
