using System.Collections;
using UnityEngine;
using WW4.EventSystem;
using WW4.Utility;

namespace WW4.Entities
{
	[RequireComponent(typeof(AudioSource)), RequireComponent(typeof(Collider))]
	public class Bird : MonoBehaviour, IHitable, IPoolable
	{
		private AudioClipUrlPair _audioClipUrlPair;
		private AudioSource _audioSource;

	    public bool IsReady => _audioClipUrlPair != null;
	    public string ClipUrl => _audioClipUrlPair?.Url;

	    public void OnHit()
		{
			MessageSystem.BirdHitEventHandler.Invoke(this);
            PrefabPool.DespawnClone(gameObject);
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
            //wait until a clip has been loaded
	        yield return new WaitUntil(() => BirdSongPool.Instance.HasAudioClips);
	        _audioClipUrlPair = BirdSongPool.GetAudioClip();

	        _audioSource.clip = _audioClipUrlPair.AudioClip;
	        _audioSource.Play();
	    }
	}
}