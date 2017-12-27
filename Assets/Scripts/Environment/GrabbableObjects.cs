using System;
using UnityEngine;
using WW4.Utility;

public class GrabbableObjects : MonoBehaviour, IGrabbable
{
    public Type GetContractorType()
    {
        return typeof(GrabbableObjects);
    }
}