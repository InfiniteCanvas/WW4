using UnityEngine;
using WW4.TestScripts;
using WW4.Utility;

namespace WW4.Entities
{
	public class IdentificationPage : MonoBehaviour, Interactable
	{
		public string BirdName;

		public void Interact(GameObject heldObject)
		{
			Bird bird = heldObject.GetComponent<Bird>();
			SoundDatabase.SoundDatabaseHandler.IdentifyAudioclip(bird.ClipUrl, BirdName);
		}
	}
}
