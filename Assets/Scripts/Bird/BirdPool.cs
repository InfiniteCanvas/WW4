using System.Collections.Generic;
using UnityEngine;
using WW4.GameWorld;

namespace WW4.Utility
{
	public class BirdPool
	{
		private GameObject _birdPool;
		private GameObject _birdPrefab;
		private Queue<Bird> _birds;

		public uint RefillThreshold { get; set; }

		public Bird GetBird()
		{
			RefillIfBelowThreshold();

			if (_birds.Count < 1)
				return CreateBird();
			else
			{
				Bird bird = _birds.Dequeue();
				bird.gameObject.SetActive(true);
				return bird;
			}
		}

		public void StoreBird(Bird bird)
		{
			bird.transform.SetParent(_birdPool.transform);			
			bird.gameObject.SetActive(false);

			_birds.Enqueue(bird);
		}

		private Bird CreateBird()
		{
			GameObject birdClone = GameObject.Instantiate(_birdPrefab);
			birdClone.transform.SetParent(_birdPool.transform);

			birdClone.SetActive(true);

			return birdClone.GetComponent<Bird>();
		}

		private void RefillIfBelowThreshold()
		{
			if (_birds.Count >= RefillThreshold) return;

			for (int i = _birds.Count; i < RefillThreshold; i++)
				StoreBird(CreateBird());
		}

		#region ctors

		public BirdPool()
		{
			_birds = new Queue<Bird>();
			_birdPrefab = PrefabCatalogue.GetPrefab("Bird");
			_birdPool = new GameObject("BirdPool");
			RefillThreshold = 5;
			RefillIfBelowThreshold();
		}

		public BirdPool(uint refillThreshold)
		{
			_birds = new Queue<Bird>();
			_birdPrefab = PrefabCatalogue.GetPrefab("Bird");
			_birdPool = new GameObject("BirdPool");
			RefillThreshold = refillThreshold;
			RefillIfBelowThreshold();
		}

		public BirdPool(uint refillThreshold, string gameObjectName)
		{
			_birds = new Queue<Bird>();
			_birdPrefab = PrefabCatalogue.GetPrefab("Bird");
			_birdPool = new GameObject(gameObjectName);
			RefillThreshold = refillThreshold;
			RefillIfBelowThreshold();
		}

		#endregion
	}
}