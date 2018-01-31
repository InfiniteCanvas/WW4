using UnityEngine;
using WW4.TestScripts;
using WW4.Utility;
using WW4.EventSystem;

namespace WW4.Entities.Book
{
	public class IdentificationPage : MonoBehaviour, IInteractable
	{
		public string BirdName;

		public void Interact(GameObject heldObject)
		{
			Bird bird = heldObject.GetComponent<Bird>();
			SoundDatabase.SoundDatabaseHandler.IdentifyAudioclip(bird.ClipUrl, BirdName);
			heldObject.GetComponent<FixedJoint> ();
			heldObject.SetActive(false);
			MessageSystem.EntityThrownEventHandler.Invoke (gameObject, gameObject.GetComponent<IGrabbable> ());
		}
	}
}
