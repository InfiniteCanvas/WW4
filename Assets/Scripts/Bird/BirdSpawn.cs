using UnityEngine;
using WW4.Utility;

namespace WW4.GameWorld
{
	[RequireComponent(typeof(SphereCollider))]
	public class BirdSpawn : MonoBehaviour
	{
		private static BirdPool _birdPool;

		private Bird _currentBird;
		public int PlayerDetectionRange;

		private void Start()
		{
			GetComponent<SphereCollider>().radius = PlayerDetectionRange;
			if (_birdPool == null) _birdPool = new BirdPool(5, "BirdPool_SpawnPoints");
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

			_currentBird = _birdPool.GetBird();
			_currentBird.transform.SetParent(transform);
			_currentBird.transform.position = transform.position;
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