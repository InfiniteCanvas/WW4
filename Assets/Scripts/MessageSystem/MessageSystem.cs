using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WW4.GameWorld;

public class MessageSystem : MonoBehaviour
{

	public static OnBirdHitEvent OnBirdHit;

}

public class OnBirdHitEvent : UnityEvent<Bird>
{
	
}
