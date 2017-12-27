using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using WW4.EventSystem;
using WW4.Utility;

namespace WW4.Entities
{
    public class TutorialBird : GrabbableObjects
    {
        public BirdName Name;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            MessageSystem.EntityGrabbedEventHandler.AddListener(OnGrabbed);
            MessageSystem.EntityThrownEventHandler.AddListener(OnThrown);
        }

        private void OnGrabbed(GameObject go, IGrabbable grabbable)
        {
            if (go == gameObject)
            {
                _audioSource.Play();
            }
        }

        private void OnThrown(GameObject go, IGrabbable grabbable)
        {
            if (go == gameObject)
            {
                _audioSource.Stop();
            }
        }

        protected override void OnJointBreak(float breakForce)
        {
            base.OnJointBreak(breakForce);
            _audioSource.Stop();
        }

        public enum BirdName
        {
            Coopy,
            Doopy,
            Poopy
        }
    }
}