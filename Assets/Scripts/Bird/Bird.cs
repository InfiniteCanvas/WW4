using System.Collections;
using UnityEngine;
using WW4.Utility;

namespace WW4.GameWorld
{
	[RequireComponent(typeof(AudioSource))]
	public class Bird : MonoBehaviour, IHitable
	{
		private AudioClipUrlPair _audioClipUrlPair;
		private AudioSource _audioSource;

		public string ClipUrl
		{
			get { return _audioClipUrlPair != null ? _audioClipUrlPair.Url : null; }
		}

		public void OnHit()
		{
			MessageSystem.OnBirdHit.Invoke(this);
		}

		public void Spawn()
		{
			gameObject.SetActive(true);
			StartCoroutine(LoadAudioClipAndPlay());
		}

		public void Despawn()
		{
			_audioClipUrlPair = null;
			gameObject.SetActive(false);
		}

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();

			_audioSource.playOnAwake = false;
			_audioSource.loop = true;
			_audioSource.Stop();
		}

		private IEnumerator LoadAudioClipAndPlay()
		{
			while (_audioClipUrlPair == null)
			{
				_audioClipUrlPair = BirdSongPool.GetAudioClip();
				yield return new WaitForEndOfFrame();
			}

			_audioSource.clip = _audioClipUrlPair.AudioClip;
			_audioSource.Play();
		}
	}
}