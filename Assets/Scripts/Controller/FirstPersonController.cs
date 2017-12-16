using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using WW4.Utility;

public class FirstPersonController : RigidbodyFirstPersonController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetButton("Fire1")) Interact();
    }

    private void Interact()
    {
        print($"Searching for interaction target...");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit,
            1 << LayerMask.NameToLayer("Interactable")))
        {
            print($"Target found! Interacting with {hit.transform.name}.");
            hit.transform.GetComponent<Interactable>().Interact();
        }
    }
}