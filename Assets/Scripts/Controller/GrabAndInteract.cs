using UnityEngine;
using WW4.Utility;

public class GrabAndInteract : MonoBehaviour
{
	private GameObject _collidingObject;
	private InputManager _controller;
	private GameObject _heldObject;
	private LayerMask _interactableMask;
	private const float MaxInteractionDistance = 2f;

	private void Start()
	{
		_interactableMask = 1 << LayerMask.NameToLayer("Interactable");
		_controller = GetComponent<InputManager>();
	}

	private void SetCollidingObject(Collider col)
	{
		if (_collidingObject || col.GetComponent<IGrabbable>()==null)
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
		_heldObject = _collidingObject;
		_collidingObject = null;
		FixedJoint joint = AddFixedJoint();
		joint.connectedBody = _heldObject.GetComponent<Rigidbody>();
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
			_heldObject.GetComponent<Rigidbody>().velocity = _controller.GetVelocity();
			_heldObject.GetComponent<Rigidbody>().angularVelocity = _controller.GetAngularVelocity();
		}
		_heldObject = null;
	}

	private void Update()
	{
		if (_controller.GetButtonDown(PlayerButtons.Grab))
			if (_collidingObject)
				GrabObject();

		if (_controller.GetButtonUp(PlayerButtons.Grab))
			if (_heldObject)
				ReleaseObject();

		if (_controller.GetButtonDown(PlayerButtons.Interact))
			Interact();
	}

	private void Interact()
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		if (Physics.Raycast(ray, out hit, MaxInteractionDistance, _interactableMask))
		{
			hit.transform.GetComponent<Interactable>().Interact(_heldObject);
		}
	}
}