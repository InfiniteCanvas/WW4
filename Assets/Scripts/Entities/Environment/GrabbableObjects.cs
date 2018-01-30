using System;
using UnityEngine;
using WW4.Utility;

//[RequireComponent(typeof(Rigidbody))]
namespace WW4.Entities
{
    [SelectionBase]
    public class GrabbableObjects : MonoBehaviour, IGrabbable
    {
        public Type GetContractorType()
        {
            return GetType();
        }

        protected virtual void OnJointBreak(float breakForce)
        {
            print($"Joint of {name} broke at break force of {breakForce}.");
        }
    }
}