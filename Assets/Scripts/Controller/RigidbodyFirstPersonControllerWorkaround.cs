using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using WW4.Utility;

public class RigidbodyFirstPersonControllerWorkaround : RigidbodyFirstPersonController
{
    public LayerMask Mask;

    protected override void Start()
    {
        base.Start();
        Debug.Log("Initializing first person controller");
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.E)) Interact();
        if (Input.GetButtonDown("Fire1")) ThrowBall();
    }

    private void Interact()
    {
        print($"Searching for interaction target...");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f, Mask))
        {
            print($"Target found! Interacting with {hit.transform.name}.");
            hit.transform.GetComponent<Interactable>()?.Interact();
        }
    }

    private void ThrowBall()
    {
        float force = 25f;
        Ball ball = PrefabPool.SpawnClone<Ball>(PrefabCatalogue.Instance["Ball"]);
        
        ball.transform.position = transform.position + transform.forward;
        ball.Rigidbody.AddForce((transform.forward+transform.up*.5f).normalized * force, ForceMode.VelocityChange);
    }
}