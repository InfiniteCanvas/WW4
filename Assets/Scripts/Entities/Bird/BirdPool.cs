using System.Collections.Generic;
using UnityEngine;
using WW4.Entities;

namespace WW4.Utility
{
	public class BirdPool
	{
		private static readonly GameObject BirdPrefab;

	    public static Bird GetBird()
	    {
            Debug.Log("Spawning bird.");
	        return PrefabPool.SpawnClone(BirdPrefab).GetComponent<Bird>();
	    }

	    public static void StoreBird(Bird bird) => PrefabPool.DespawnClone(bird.gameObject);

	    static BirdPool()
		{
		    Debug.Log("Initializing birdpool");
            BirdPrefab = PrefabCatalogue.Instance["Bird"];
		}
	}
}