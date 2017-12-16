using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WW4.GameWorld;

public class MessageSystem : MonoBehaviour, InitializerControlled
{

	public static BirdHitEvent BirdHitHandler;
    public static ReturningToPool ReturningToPoolHandler;

    public bool Initialize()
    {
        BirdHitHandler = new BirdHitEvent();
        BirdHitHandler.AddListener(x=>Debug.Log($"{x.ClipUrl} was hit"));
        ReturningToPoolHandler = new ReturningToPool();
        ReturningToPoolHandler.AddListener(x=>Debug.Log($"{x.name} returned to its pool"));
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

public class ReturningToPool : UnityEvent<GameObject>
{
}
