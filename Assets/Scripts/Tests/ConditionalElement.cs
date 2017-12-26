using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW4.EventSystem;

namespace WW4.TestScripts
{
    public class ConditionalElement : MonoBehaviour, IConditionalNodeElement
    {
        public bool ConditionFulfilled()
        {
            Debug.Log($"Space pressed {Input.GetKey(KeyCode.Space)}");
            return Input.GetKey(KeyCode.Space);
        }
    }
}