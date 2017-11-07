using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW4.Database;
using WW4.TestScripts;

namespace WW4.Utility
{
	public class BirdSongPool : MonoBehaviour
	{
		private Queue<AudioClipUrlPair> _birdSongs;
		private int _downloadsInProgress;

		private SoundDatabaseHandler _soundDatabase;
		public uint RefillThreshold = 5;

		public static BirdSongPool Instance { private set; get; }

		private void Start()
		{
			if (Instance == null)
			{
				Instance = this;
				_soundDatabase = SoundDatabase.SoundDatabaseHandler;
				_birdSongs = new Queue<AudioClipUrlPair>();		
			RefillIfBelowThreshold();

			}
			else
			{
				Destroy(this);
			}
		}

		private IEnumerator DownloadAudioclips()
		{
			_downloadsInProgress++;
			AudioClipUrlPair birdsong = new AudioClipUrlPair {Url = _soundDatabase.GetAudioClipUrl()};
			WWW download = new WWW(birdsong.Url);

			print("Downloading "+birdsong.Url);

			while (!download.isDone)
				yield return null;
			birdsong.AudioClip = download.GetAudioClip();

			_birdSongs.Enqueue(birdsong);

			download.Dispose();
			_downloadsInProgress--;
		}

		private void RefillIfBelowThreshold()
		{
			if (_downloadsInProgress >= RefillThreshold - _birdSongs.Count) return;
			for (int i = 0; i < RefillThreshold - _birdSongs.Count; i++)
				StartCoroutine(DownloadAudioclips());
		}

		//kind of hacky, but I wanted it static
		public static AudioClipUrlPair GetAudioClip()
		{
			Instance.RefillIfBelowThreshold();

			return Instance._birdSongs.Count < 1 ? null : Instance._birdSongs.Dequeue();
		}
	}

	public class AudioClipUrlPair
	{
		public AudioClip AudioClip;
		public string Url;
	}
}