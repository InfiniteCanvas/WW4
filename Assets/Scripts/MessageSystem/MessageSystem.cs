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
        public static EntityThrownEvent EntityThrownEventHandler { get; private set; }
        public static NodeTraverserEvent NodeTraverserEventHandler { get; private set; }
        public static CanInteract CanInteractEventHandler { get; private set; }
        public static CanGrabEvent CanGrabEventHandler { get; private set; }

        public bool Initialize()
        {
            BirdHitEventHandler = new BirdHitEvent();
            BirdHitEventHandler.AddListener(x=>Debug.Log($"{x.ClipUrl} was hit"));
            ReturningToPoolEventHandler = new ReturningToPoolEvent();
            ReturningToPoolEventHandler.AddListener(x=>Debug.Log($"{x.name} returned to its pool"));
            EntityGrabbedEventHandler = new EntityGrabbedEvent();
            EntityGrabbedEventHandler.AddListener((go,grabbable) => Debug.Log($"GameObject [{go.name}] of type [{grabbable.GetContractorType().FullName}] was grabbed."));
            EntityThrownEventHandler = new EntityThrownEvent();
            EntityThrownEventHandler.AddListener((go, grabbable) => Debug.Log($"GameObject [{go.name}] of type [{grabbable.GetContractorType().FullName}] was thrown."));
            NodeTraverserEventHandler = new NodeTraverserEvent();
            NodeTraverserEventHandler.AddListener((x,y)=>Debug.Log($"{nameof(NodeTraverserEvent)} was called in Node: {x.name} in system: {x.EventSystemID}\nArgs: {y.TargetNode}"));
            CanInteractEventHandler = new CanInteract();
            CanGrabEventHandler = new CanGrabEvent();

            return true;
        }

        public Type GetClass()
        {
            return typeof(MessageSystem);
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

    public class EntityThrownEvent : UnityEvent<GameObject, IGrabbable>
    {
    }

    public class NodeTraverserEvent : UnityEvent<EventNode, NodeTraverserEventArgs>
    {
    }

    public class CanGrabEvent : UnityEvent<IGrabbable, GameObject>
    {
    }

    public class CanInteract : UnityEvent<IInteractable>
    {
    }

    public class NodeTraverserEventArgs : EventArgs
    {
        public NodeTraverserEventArgs(EventNode targetNode=null)
        {
            TargetNode = targetNode;
        }

        public EventNode TargetNode { get; }
        public bool HasTargetNode => TargetNode != null;
    }
}
