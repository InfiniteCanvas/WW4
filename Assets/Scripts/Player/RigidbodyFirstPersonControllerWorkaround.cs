using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using WW4.Entities;
using WW4.EventSystem;
using WW4.Utility;

public class RigidbodyFirstPersonControllerWorkaround : RigidbodyFirstPersonController
{
    public LayerMask Mask = 1 << 7;
    public Rigidbody GrabRigidbody;
    public float ThrowForce = 25;
    public float BreakForce = 20000;

    private GameObject _ballPrefab;
    private GameObject BallPrefab => _ballPrefab ?? (_ballPrefab = PrefabPool.SpawnClone(PrefabCatalogue.Instance["Ball"]));

    private FixedJoint _fixedJoint;
    private Vector3 MiddleScreenRay => Camera.main.ViewportPointToRay(new Vector2(.5f, .5f)).direction;

    public bool IsHoldingObject => _fixedJoint != null;

    protected override void Start()
    {
        base.Start();
        MessageSystem.CanInteractEventHandler.AddListener(Interact);
        MessageSystem.CanGrabEventHandler.AddListener(Grab);
    }

    protected override void Update()
    {
        base.Update();

        CheckInteractables();

        if (Input.GetButtonDown("Fire1")) Throw();
    }

    private void CheckInteractables()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        if (Physics.Raycast(ray, out hit, 3f, Mask))
        {
            var interactable = hit.transform.GetComponent<IInteractable>();
            var grabbable = hit.transform.GetComponent<IGrabbable>();

            if(interactable!=null)
                MessageSystem.CanInteractEventHandler.Invoke(interactable);
            if(grabbable!=null)
                MessageSystem.CanGrabEventHandler.Invoke(grabbable, hit.transform.gameObject);
        }
    }

    private void Interact(IInteractable interactable)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_fixedJoint != null)
                interactable.Interact(_fixedJoint?.gameObject);
            else
                interactable.Interact(); 
        }
    }

    private void Throw()
    {
        if (IsHoldingObject)
        {
            ThrowGrabbedObject();
        }
        else
        {
            ThrowBall();
        }
    }

    private void Grab(IGrabbable grabbable, GameObject grabbableGameObject)
    {
        if (Input.GetButtonDown("Fire2"))
        {
            grabbableGameObject.transform.position = GrabRigidbody.position;
            _fixedJoint = AddOrGetFixedJoint(grabbableGameObject);
            _fixedJoint.connectedBody = GrabRigidbody;
            MessageSystem.EntityGrabbedEventHandler.Invoke(grabbableGameObject, grabbable);
        }
    }

    private FixedJoint AddOrGetFixedJoint(GameObject go)
    {
        FixedJoint fj = go.AddComponent<FixedJoint>();
        fj.breakForce = BreakForce;
        fj.breakTorque = BreakForce;

        return fj;
    }

    private void ThrowGrabbedObject()
    {
        MessageSystem.EntityThrownEventHandler.Invoke(_fixedJoint.gameObject, _fixedJoint.GetComponent<IGrabbable>());

        Rigidbody rb = _fixedJoint.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Destroy(_fixedJoint);
            rb.AddForce(MiddleScreenRay * ThrowForce, ForceMode.VelocityChange);
        }
        else
        {
            rb = _fixedJoint.gameObject.AddComponent<Rigidbody>();
            Destroy(_fixedJoint);
            rb.AddForce(MiddleScreenRay * ThrowForce, ForceMode.VelocityChange);
            Destroy(rb, 5f);
        }
    }

    private void ThrowBall()
    {
        Ball ball = PrefabPool.SpawnClone<Ball>(BallPrefab);
        
        ball.transform.position = transform.position + transform.forward + Vector3.up*1.5f;
        ball.Rigidbody.AddForce(MiddleScreenRay * ThrowForce, ForceMode.VelocityChange);
    }
}