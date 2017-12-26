using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW4.Utility;

namespace WW4.Entities
{
    public class Door : MonoBehaviour, IInteractable
    {
        private bool _isOpen;
        private bool _isMoving;

        public float RotationSpeed=120;

        public void Interact(GameObject heldObject = null)
        {
            if (_isOpen)
            {
                if (!_isMoving)
                {
                    StartCoroutine(CloseDoor());
                }
            }
            else
            {
                if (!_isMoving)
                {
                    StartCoroutine(OpenDoor());
                }
            }
        }

        private IEnumerator OpenDoor()
        {
            _isMoving = true;

            for (float i = 0; i < 120 / RotationSpeed; i += Time.deltaTime)
            {
                transform.localEulerAngles = new Vector3(-90, -i*RotationSpeed, 0);
                yield return null;
            }

            transform.localEulerAngles = new Vector3(-90, -120, 0);

            _isOpen = true;
            _isMoving = false;
        }

        private IEnumerator CloseDoor()
        {
            _isMoving = true;

            for (float i = 0; i < 120 / RotationSpeed; i += Time.deltaTime)
            {
                transform.localEulerAngles = new Vector3(-90, -120+i * RotationSpeed, 0);
                yield return null;
            }

            transform.localEulerAngles = new Vector3(-90, 0, 0);

            _isOpen = false;
            _isMoving = false;
        }
    }
}