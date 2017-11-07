using UnityEngine;

namespace WW4.Utility
{
	public interface IControllerInput
	{
		bool GetButtonDown(PlayerButtons playerButton);
		bool GetButtonUp(PlayerButtons playerButton);
		bool GetButton(PlayerButtons playerButton);

		Vector2 GetAxis();

		Vector3 GetVelocity();
		Vector3 GetAngularVelocity();
	}

	public enum PlayerButtons
	{
		Interact,
		Grab,
		Teleport
	}
}