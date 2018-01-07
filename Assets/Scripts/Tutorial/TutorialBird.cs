using UnityEngine;
using WW4.Entities;
using WW4.EventSystem;
using WW4.Utility;


namespace WW4.Tutorial
{
    public class TutorialBird : GrabbableObjects
    {
        public enum BirdName
        {
            Coopy,
            Doopy,
            Poopy
        }

        private Vector3 _position;
        private Quaternion _rotation;
        private AudioSource _audioSource;
        public BirdName Name;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            MessageSystem.EntityGrabbedEventHandler.AddListener(OnGrabbed);
            MessageSystem.EntityThrownEventHandler.AddListener(OnThrown);
            _position = transform.localPosition;
            _rotation = transform.localRotation;
            gameObject.SetActive(false);
        }

        private void OnGrabbed(GameObject go, IGrabbable grabbable)
        {
            if (go == gameObject)
                _audioSource.Play();
        }

        private void OnThrown(GameObject go, IGrabbable grabbable)
        {
            if (go == gameObject)
                _audioSource.Stop();
        }

        protected override void OnJointBreak(float breakForce)
        {
            base.OnJointBreak(breakForce);
            _audioSource.Stop();
        }

        public void ResetAndActivate()
        {
            transform.localPosition = _position;
            transform.localRotation = _rotation;
            gameObject.SetActive(true);
        }
    }
}