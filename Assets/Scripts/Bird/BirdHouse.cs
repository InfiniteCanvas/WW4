using System.Collections;
using UnityEngine;
using WW4.Utility;

namespace WW4.GameWorld
{
    [RequireComponent(typeof(SphereCollider))]
    public class BirdHouse : MonoBehaviour, IInteractable
    {
	    public Transform SpawnPoint;

        private Bird _currentBird;

        public void Interact(GameObject heldObject)
        {
			if(_currentBird==null)
				SpawnBird();
			else
				StoreBird();
        }

        private void OnTriggerExit(Collider other)
        {
			if(other.CompareTag("Player"))
				StoreBird();
        }

        private void SpawnBird()
        {
	        if (_currentBird != null) return;

            _currentBird = BirdPool.GetBird();
            _currentBird.transform.SetParent(transform);
            _currentBird.transform.position = SpawnPoint.position;
            _currentBird.gameObject.SetActive(true);
        }

        private void StoreBird()
        {
            if (_currentBird == null) return;

            BirdPool.StoreBird(_currentBird);
            _currentBird = null;
        }
    }
}