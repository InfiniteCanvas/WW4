using System.Collections.Generic;
using UnityEngine;
using WW4.GameWorld;

namespace WW4.Utility
{
	public class BirdPool
	{
		private static readonly GameObject BirdPrefab;

		public static Bird GetBird() => PrefabPool.SpawnClone(BirdPrefab).GetComponent<Bird>();

	    public static void StoreBird(Bird bird) => PrefabPool.DespawnClone(bird.gameObject);

	    static BirdPool()
		{
			BirdPrefab = PrefabCatalogue.GetPrefab("Bird");
		}
	}
}