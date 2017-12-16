using System;
using System.Linq;
using UnityEngine;

namespace WW4.Utility
{
	[CreateAssetMenu(fileName = "PrefabCatalogue", menuName = "PrefabCatalogue")]
	public class PrefabCatalogue : ScriptableObject
	{
		[SerializeField] private StringGameObjectPair[] _dictionary;
		public static PrefabCatalogue Instance { get; private set; }

		private void OnEnable()
		{
			Debug.Assert(Instance == null, "There should be only one PrefabCatalogue.");
			Instance = this;
		}

		//it's hacky, but I want it as static
		public static GameObject GetPrefab(string name)
		{
		    return Instance._dictionary.FirstOrDefault(x => x.Name == name)?.Prefab;
		}
	}

	[Serializable]
	public class StringGameObjectPair
	{
		public string Name;
		public GameObject Prefab;
	}
}