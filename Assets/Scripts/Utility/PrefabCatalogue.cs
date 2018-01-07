using System;
using System.Linq;
using UnityEngine;

namespace WW4.Utility
{
	public class PrefabCatalogue : MonoBehaviour, InitializerControlled
	{
		[SerializeField] private StringGameObjectPair[] _dictionary;
		public static PrefabCatalogue Instance { get; private set; }

		//it's hacky, but I want it as static
		public GameObject GetPrefab(string prefabName)
		{
		    return _dictionary.FirstOrDefault(x => x.Name == prefabName)?.Prefab;
		}

	    public GameObject this[string prefabName] => GetPrefab(prefabName);

	    public bool Initialize()
	    {
	        if (Instance == null)
	        {
	            Instance = this;
	            return true;	            
	        }
	        else
	        {
	            return false;
	        }
	    }

	    public Type GetClass()
	    {
	        return typeof(PrefabCatalogue);
	    }
	}

	[Serializable]
	public class StringGameObjectPair
	{
		public string Name;
		public GameObject Prefab;
	}
}