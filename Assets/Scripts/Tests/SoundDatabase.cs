#define TESTING

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using WW4.Database;
using Newtonsoft.Json;

namespace WW4.TestScripts
{
	public class SoundDatabase : MonoBehaviour, SoundDatabaseHandler
	{
		private string[] Urls;

		private Dictionary<string, string> _identifiedSounds;

		public static SoundDatabaseHandler SoundDatabaseHandler { private set; get; }

		public string GetAudioClipUrl()
		{
			return Urls[Random.Range(0, Urls.Length)];
		}

		private void GetAllFilePaths()
		{
			Urls = Directory.GetFiles(Application.streamingAssetsPath, "*.ogg");
		}

		public bool IdentifyAudioclip(string clipUrl, string name)
		{
			string fileName = clipUrl.Remove(0, Application.streamingAssetsPath.Length + 1);
			if (!_identifiedSounds.ContainsKey(fileName))
			{
				_identifiedSounds.Add(fileName, name);
				SerializeDatabase();
				return true;
			}

			//already exists
			return false;
		}

		private void Awake()
		{
			SoundDatabaseHandler = this;
			GetAllFilePaths();
			DeserializeDatabase();
			PrintAllIdentifiedBirds();
		}

		private void OnApplicationQuit()
		{
			SerializeDatabase();
		}

		private void DeserializeDatabase()
		{
			if (File.Exists(Application.dataPath + "/IdentifiedSounds"))
				_identifiedSounds =
					JsonConvert.DeserializeObject<Dictionary<string, string>>(
						File.ReadAllText(Application.dataPath + "/IdentifiedSounds"));
			if(_identifiedSounds==null)
				_identifiedSounds = new Dictionary<string, string>();
		}

		private void SerializeDatabase()
		{
			string s = JsonConvert.SerializeObject(_identifiedSounds);
			File.WriteAllText(Application.dataPath+"/IdentifiedSounds", s);
		}

#if TESTING
		public void PrintAllIdentifiedBirds()
		{
			foreach (var identifiedSound in _identifiedSounds)
			{
				print(string.Format("{0} : {1}", identifiedSound.Key, identifiedSound.Value));
			}
		}
#endif
	}
}