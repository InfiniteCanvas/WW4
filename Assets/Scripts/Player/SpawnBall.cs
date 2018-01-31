using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW4.Utility;
using WW4.Entities;


public class SpawnBall : MonoBehaviour {

	private InputManager _controller;
	private GrabAndInteract _grabAndInteract;
	private GameObject _ballPrefab;
	private GameObject BallPrefab => _ballPrefab ?? (_ballPrefab = PrefabPool.SpawnClone(PrefabCatalogue.Instance["Ball"]));

	void Start () {
		_controller = GetComponent<InputManager>();
		_grabAndInteract = GetComponent<GrabAndInteract> ();
	}
	
	void Update () {
		if (_controller.GetButtonDown (PlayerButtons.SpawnBall))
		if(!_grabAndInteract.IsHoldingObject)
			_grabAndInteract.GrabObject (GetBall ());

		if (_controller.GetButtonUp (PlayerButtons.SpawnBall))
			_grabAndInteract.ReleaseObject();
	}

	private GameObject GetBall()
	{
		Ball ball = PrefabPool.SpawnClone<Ball>(BallPrefab);
		return ball.gameObject;
	}
}
