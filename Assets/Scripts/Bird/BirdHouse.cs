using UnityEngine;
using WW4.Utility;

namespace WW4.Entities
{
    [RequireComponent(typeof(SphereCollider))]
    public class BirdHouse : MonoBehaviour, Interactable
    {
	    public Transform SpawnPoint;

        private Bird _currentBird;

        private void Start()
        {
            MessageSystem.ReturningToPoolEventHandler.AddListener(OnReturningToPool);
        }

        private void OnReturningToPool(GameObject go)
        {
            if (_currentBird == null) return;

            if (go == _currentBird.gameObject)
                _currentBird = null;
        }

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