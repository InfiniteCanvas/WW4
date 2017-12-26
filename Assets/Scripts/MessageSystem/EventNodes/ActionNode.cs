﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW4.EventSystem
{
    public class ActionNode : EventNode
    {
        private void Awake()
        {
            OnActivation += Action;
        }

        private void Action()
        {
            Debug.Log($"End reached. Root is {Root.name}. EventSystemID is {EventSystemID}.");
        }
    }
}