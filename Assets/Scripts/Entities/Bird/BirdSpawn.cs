using UnityEngine;
using WW4.Utility;

namespace WW4.Entities
{
	[RequireComponent(typeof(SphereCollider))]
	public class BirdSpawn : MonoBehaviour
	{
        private Bird _currentBird;

		private void Start()
		{
			//GetComponent<SphereCollider>().radius = PlayerDetectionRange;
		}

		private void OnTriggerEnter(Collider other)
		{			
			if(other.CompareTag("Player"))
				SpawnBird();
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
			_currentBird.transform.position = transform.position;
		}

		private void StoreBird()
		{
			if (_currentBird == null) return;

			BirdPool.StoreBird(_currentBird);
			_currentBird = null;
		}
	}
}