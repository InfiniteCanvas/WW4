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

    private bool IsHoldingObject => _fixedJoint != null;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.E)) Interact();
        if (Input.GetButtonDown("Fire1")) ThrowBall();
        if(Input.GetButtonDown("Fire2")) GrabOrThrow();
    }

    private void Interact()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        if (Physics.Raycast(ray, out hit, 3f, Mask))
        {
            var interactable = hit.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                //print($"Target found! Interacting with {hit.transform.name}.");
                if (_fixedJoint != null)
                    interactable.Interact(_fixedJoint?.gameObject);
                else
                    interactable.Interact();
            }
        }
    }

    private void GrabOrThrow()
    {
        if (IsHoldingObject)
        {
            ThrowGrabbedObject();
        }
        else
        {
            Grab();
        }
    }

    private void Grab()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        if (Physics.Raycast(ray, out hit, 3f, Mask))
        {
            var grabbable = hit.transform.GetComponent<IGrabbable>();
            if (grabbable != null)
            {                
                //print($"Target found! Grabbing {hit.transform.name} of type {grabbable.GetContractorType()}.");
                hit.transform.position = GrabRigidbody.position;
                _fixedJoint = AddOrGetFixedJoint(hit.transform.gameObject);
                _fixedJoint.connectedBody = GrabRigidbody;
                MessageSystem.EntityGrabbedEventHandler.Invoke(hit.transform.gameObject, grabbable);
            }
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