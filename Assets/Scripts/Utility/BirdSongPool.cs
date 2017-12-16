using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW4.Database;
using WW4.TestScripts;
using System.IO;

namespace WW4.Utility
{
	public class BirdSongPool : MonoBehaviour, InitializerControlled
	{
		private Queue<AudioClipUrlPair> _birdSongs;
		private int _downloadsInProgress;

		private SoundDatabaseHandler _soundDatabase;
		public uint RefillThreshold = 5;

		public static BirdSongPool Instance { private set; get; }
        public bool HasAudioClips => _birdSongs.Count > 0;

		private IEnumerator DownloadAudioclip()
		{
			_downloadsInProgress++;
			string url = _soundDatabase.GetAudioClipUrl();

			WWW download = new WWW(url);
			yield return download;

            AudioClip clip = download.GetAudioClip();

			_birdSongs.Enqueue(new AudioClipUrlPair(url, clip));

			download.Dispose();
			_downloadsInProgress--;
		}

		private void RefillIfBelowThreshold()
		{
			if (_downloadsInProgress >= RefillThreshold - _birdSongs.Count) return;
			for (int i = 0; i < RefillThreshold - _birdSongs.Count; i++)
				StartCoroutine(DownloadAudioclip());
		}

		//kind of hacky, but I wanted it static
		public static AudioClipUrlPair GetAudioClip()
		{
			Instance.RefillIfBelowThreshold();
			return Instance._birdSongs.Count < 1 ? null : Instance._birdSongs.Dequeue();
		}

		private AudioClipUrlPair GetAudioclipFromStreamingAssets()
		{
			var files = Directory.GetFiles(Application.streamingAssetsPath);
			var url = $"file://{files[Random.Range(0, files.Length)]}";
			var clip = new WWW(url).GetAudioClip(false, false, AudioType.OGGVORBIS);

			return new AudioClipUrlPair(url, clip);
		}

	    public bool Initialize()
	    {
	        if (Instance == null)
	        {
	            Instance = this;
	            _soundDatabase = SoundDatabase.SoundDatabaseHandler;
	            _birdSongs = new Queue<AudioClipUrlPair>();
	            RefillIfBelowThreshold();
	            return true;
	        }
	        else
	        {
	            return false;
	        }
        }

	    public string GetClassName()
	    {
	        return typeof(BirdSongPool).FullName;
	    }
	}

	public class AudioClipUrlPair
	{
		public AudioClip AudioClip { get; private set; }
		public string Url { get; private set; }

		public AudioClipUrlPair(string url, AudioClip clip)
		{
			AudioClip = clip;
			Url = url;
		}
	}
}