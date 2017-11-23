using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WW4.GameWorld;

public class MessageSystem : MonoBehaviour
{

	public static OnBirdHitEvent OnBirdHit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class OnBirdHitEvent : UnityEvent<Bird>
{
	
}
