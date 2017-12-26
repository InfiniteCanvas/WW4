using System;
using System.Collections.Generic;
using UnityEngine;
using WW4.EventSystem;

//Assuming only one component on gameObject implements IPoolable
namespace WW4.Utility
{
    public static class PrefabPool
    {
        private static readonly Dictionary<GameObject, Pool> Pools = new Dictionary<GameObject, Pool>();
        private static readonly Dictionary<GameObject, Pool> ActiveObjects = new Dictionary<GameObject, Pool>();

#if UNITY_EDITOR
        public static int NumberOfActiveObjects => ActiveObjects.Count;
#endif

        public static GameObject SpawnClone(GameObject prefab)
        {
            if (!Pools.ContainsKey(prefab))
            {
                if(prefab.GetComponent<IPoolable>()!=null)
                    Pools.Add(prefab, new Pool(prefab));
                else
                {
                    throw new ArgumentException($"The used prefab does not implement the {nameof(IPoolable)} interface!");
                }
            }                

            GameObject clone = Pools[prefab].GetClone();
            ActiveObjects.Add(clone, Pools[prefab]);
            clone.GetComponent<IPoolable>().Spawn();

            return clone;
        }

        public static T SpawnClone<T>(GameObject prefab) where T : MonoBehaviour
        {
            return SpawnClone(prefab).GetComponent<T>();
        }

        public static void DespawnClone(GameObject clone)
        {
            if (ActiveObjects.ContainsKey(clone))
            {
                clone.GetComponent<IPoolable>().Despawn();
                MessageSystem.ReturningToPoolEventHandler.Invoke(clone);
                ActiveObjects[clone].ReturnClone(clone);
                ActiveObjects.Remove(clone);
            }
            else
            {
                Debug.LogWarning($"{clone.name} is not managed by the pool.");
                //Object.Destroy(clone);
            }
        }
    }
}
