using System.Collections;
using UnityEngine;
using WW4.Utility;

namespace WW4.GameWorld
{
    [RequireComponent(typeof(SphereCollider))]
    public class BirdHouse : MonoBehaviour, IInteractable
    {
        private static BirdPool _birdPool;

	    public Transform SpawnPoint;

        private Bird _currentBird;

        public void Interact(object actor)
        {
            Interact();
        }

        public void Interact()
        {
			if(_currentBird==null)
				SpawnBird();
			else
				StoreBird();
        }

		private void Start()
        {
            if (_birdPool == null) _birdPool = new BirdPool(2,"BirdPool_BirdHouses");
        }

        private void OnTriggerExit(Collider other)
        {
			if(other.CompareTag("Player"))
				StoreBird();
        }

        private void SpawnBird()
        {
	        if (_currentBird != null) return;

            _currentBird = _birdPool.GetBird();
            _currentBird.transform.SetParent(transform);
            _currentBird.transform.position = SpawnPoint.position;
            _currentBird.gameObject.SetActive(true);
        }

        private void StoreBird()
        {
            if (_currentBird == null) return;

            _birdPool.StoreBird(_currentBird);
            _currentBird = null;
        }
    }
}