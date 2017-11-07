using UnityEngine;
using WW4.Utility;

public class Grab : MonoBehaviour
{
	private GameObject _collidingObject;
	private InputManager _controller;
	private GameObject _objectInHand;

	private void Start()
	{
		_controller = GetComponent<InputManager>();
	}

	private void SetCollidingObject(Collider col)
	{
		if (_collidingObject || !col.GetComponent<Rigidbody>())
			return;
		_collidingObject = col.gameObject;
	}

	public void OnTriggerEnter(Collider other)
	{
		SetCollidingObject(other);
	}

	public void OnTriggerStay(Collider other)
	{
		SetCollidingObject(other);
	}

	public void OnTriggerExit(Collider other)
	{
		if (!_collidingObject)
			return;

		_collidingObject = null;
	}

	private void GrabObject()
	{
		_objectInHand = _collidingObject;
		_collidingObject = null;
		FixedJoint joint = AddFixedJoint();
		joint.connectedBody = _objectInHand.GetComponent<Rigidbody>();
	}

	private FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	private void ReleaseObject()
	{
		if (GetComponent<FixedJoint>())
		{
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());
			_objectInHand.GetComponent<Rigidbody>().velocity = _controller.GetVelocity();
			_objectInHand.GetComponent<Rigidbody>().angularVelocity = _controller.GetAngularVelocity();
		}
		_objectInHand = null;
	}

	private void Update()
	{
		if (_controller.GetButtonDown(PlayerButtons.Grab))
			if (_collidingObject)
				GrabObject();

		if (_controller.GetButtonUp(PlayerButtons.Grab))
			if (_objectInHand)
				ReleaseObject();
	}
}