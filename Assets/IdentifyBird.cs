using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW4.EventSystem;
using WW4.Utility;
using WW4.Entities;

public class IdentifyBird : MonoBehaviour {

	public GameObject IdentificationPages;

	private GrabAndInteract _grabber;
	void Start () {
		_grabber = GetComponent<GrabAndInteract> ();
		MessageSystem.BirdHitEventHandler.AddListener (StartIdentification);
		MessageSystem.EntityThrownEventHandler.AddListener (StopIdentification);
	}
	
	private void StartIdentification(Bird bird)
	{
		IdentificationPages.SetActive (true);
	}

	private void StopIdentification(GameObject go, IGrabbable grabbable)
	{

	}
}
