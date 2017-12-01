using System.Collections.Generic;
using UnityEngine;
using WW4.GameWorld;

namespace WW4.Utility
{
	public class BirdPool
	{
		private static GameObject _birdPoolParentObject;
		private static GameObject _birdPrefab;
		private static Queue<Bird> _birds;

		public static Bird GetBird()
		{
			if (_birds.Count < 1)
			{
				Bird bird = CreateBird();
				bird.Spawn();
				return bird;
			}				
			else
			{
				Bird bird = _birds.Dequeue();
				bird.Spawn();
				return bird;
			}
		}

		public static void StoreBird(Bird bird)
		{
			bird.transform.SetParent(_birdPoolParentObject.transform);			
			bird.Despawn();

			_birds.Enqueue(bird);
		}

		private static Bird CreateBird()
		{
			GameObject birdClone = Object.Instantiate(_birdPrefab);
			birdClone.transform.SetParent(_birdPoolParentObject.transform);

			return birdClone.GetComponent<Bird>();
		}

		static BirdPool()
		{
			_birds = new Queue<Bird>();
			_birdPrefab = PrefabCatalogue.GetPrefab("Bird");
			_birdPoolParentObject = new GameObject("BirdPool");
		}
	}
}