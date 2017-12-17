using UnityEngine;
using UnityEngine.Events;
using WW4.Entities;
using WW4.Utility;

public class MessageSystem : MonoBehaviour, InitializerControlled
{

	public static BirdHitEvent BirdHitEventHandler { get; private set; }
    public static ReturningToPoolEvent ReturningToPoolEventHandler { get; private set; }
    public static EntityGrabbedEvent EntityGrabbedEventHandler { get; private set; }

    public bool Initialize()
    {
        BirdHitEventHandler = new BirdHitEvent();
        BirdHitEventHandler.AddListener(x=>Debug.Log($"{x.ClipUrl} was hit"));
        ReturningToPoolEventHandler = new ReturningToPoolEvent();
        ReturningToPoolEventHandler.AddListener(x=>Debug.Log($"{x.name} returned to its pool"));
        EntityGrabbedEventHandler = new EntityGrabbedEvent();

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

public class EntityGrabbedEvent : UnityEvent<IGrabbable>
{
}
