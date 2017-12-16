using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WW4.Utility
{
    public class Pool
    {
        private readonly GameObject _prefab;
        private readonly Transform _poolParent;
        private readonly Queue<GameObject> _inactive;
        private readonly List<GameObject> _active;

        public Pool(GameObject prefab)
        {
            _prefab = prefab;
            _inactive = new Queue<GameObject>();
            _active = new List<GameObject>();
            _poolParent = new GameObject($"{_prefab.name}_Pool").transform;
            Object.DontDestroyOnLoad(_poolParent);
        }

        public GameObject GetClone()
        {
            GameObject go;

            if (_inactive.Count > 0)
                go = _inactive.Dequeue();
            else
            {
                go = Object.Instantiate(_prefab);
                go.transform.SetParent(_poolParent);
            }

            _active.Add(go);

            return go;
        }

        public void ReturnClone(GameObject go)
        {
            if (_active.Remove(go))
                _inactive.Enqueue(go);
            else
                Debug.LogError($"GameObject {go} is not managed by the pool.");
        }
    }
}