using System;
using UnityEngine;
using WW4.Utility;
using WW4.EventSystem;

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
			MessageSystem.EntityThrownEventHandler.Invoke (gameObject, this);
        }
    }
}