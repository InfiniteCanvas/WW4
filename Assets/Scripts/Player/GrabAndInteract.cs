using UnityEngine;
using WW4.EventSystem;
using WW4.Utility;

public class GrabAndInteract : MonoBehaviour
{
    public float ThrowForceMultiplier = 1;
	public bool IsHoldingObject => _heldObject != null;
	public float MaxInteractionDistance = 5f;

	private GameObject _collidingObject;
	private InputManager _controller;
	private GameObject _heldObject;
	private LayerMask _interactableMask;

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

        MessageSystem.EntityGrabbedEventHandler.Invoke(_heldObject, _heldObject.GetComponent<IGrabbable>());
	}

	public void GrabObject(GameObject go)
	{
		go.transform.position = transform.position;
		_heldObject = go;
		_collidingObject = null;
		FixedJoint joint = AddFixedJoint();
		joint.connectedBody = _heldObject.GetComponent<Rigidbody>();

		MessageSystem.EntityGrabbedEventHandler.Invoke(_heldObject, _heldObject.GetComponent<IGrabbable>());
	}

	private FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	public void ReleaseObject()
	{
		if (GetComponent<FixedJoint>())
		{
            MessageSystem.EntityThrownEventHandler.Invoke(_heldObject, _heldObject.GetComponent<IGrabbable>());
            var fj = GetComponent<FixedJoint>();
            fj.connectedBody = null;
			Destroy(fj);
		    var rb = _heldObject.GetComponent<Rigidbody>();
			rb.velocity = _controller.GetVelocity() * ThrowForceMultiplier;
			rb.angularVelocity = _controller.GetAngularVelocity() * ThrowForceMultiplier;
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
			hit.transform.GetComponent<IInteractable>().Interact(_heldObject);
		}
	}
}