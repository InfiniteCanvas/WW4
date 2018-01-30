using System;
using UnityEngine;

namespace WW4.Utility
{
	public class InputManager : MonoBehaviour, IControllerInput
	{
		private void Awake()
		{
			GetTrackedObject();
		}

		public void PlayHapticPulse()
		{
			Controller.TriggerHapticPulse();
		}

		#region Controller-Initiation

		private SteamVR_TrackedObject trackedObj;

		private SteamVR_Controller.Device Controller
		{
			get { return SteamVR_Controller.Input((int) trackedObj.index); }
		}

		private void GetTrackedObject()
		{
			trackedObj = GetComponent<SteamVR_TrackedObject>();
		}

		#endregion

		#region Helper Methods

		private delegate bool GetInput();

		private bool GetGripUp()
		{
			return Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip);
		}

		private bool GetGripDown()
		{
			return Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip);
		}

		private bool GetGrip()
		{
			return Controller.GetPress(SteamVR_Controller.ButtonMask.Grip);
		}

		private bool GetTouchUp()
		{
			return Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad);
		}

		private bool GetTouchDown()
		{
			return Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad);
		}

		private bool GetTouch()
		{
			return Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad);
		}

		#endregion

		#region Implementation

		public bool GetButtonUp(PlayerButtons playerButton)
		{
			GetInput input;
			switch (playerButton)
			{
				case PlayerButtons.Grab:
					input = GetGripUp;
					break;

				case PlayerButtons.Interact:
					input = Controller.GetHairTriggerUp;
					break;

				case PlayerButtons.Teleport:
					input = GetTouchUp;
					break;
				default:
					throw new ArgumentOutOfRangeException("playerButton", playerButton, null);
			}

			return input();
		}

		public bool GetButtonDown(PlayerButtons playerButton)
		{
			GetInput input;
			switch (playerButton)
			{
				case PlayerButtons.Grab:
					input = GetGripDown;
					break;

				case PlayerButtons.Interact:
					input = Controller.GetHairTriggerDown;
					break;

				case PlayerButtons.Teleport:
					input = GetTouchDown;
					break;
				case PlayerButtons.SpawnBall:
					input = GetTouchDown;
					break;
				default:
					throw new ArgumentOutOfRangeException("playerButton", playerButton, null);
			}

			return input();
		}

		public bool GetButton(PlayerButtons playerButton)
		{
			GetInput input;
			switch (playerButton)
			{
				case PlayerButtons.Grab:
					input = GetGrip;
					break;

				case PlayerButtons.Interact:
					input = Controller.GetHairTrigger;
					break;

				case PlayerButtons.Teleport:
					input = GetTouch;
					break;
				default:
					throw new ArgumentOutOfRangeException("playerButton", playerButton, null);
			}

			return input();
		}

		public Vector2 GetAxis()
		{
			return Controller.GetAxis();
		}

		public Vector3 GetVelocity()
		{
			return Controller.velocity;
		}

		public Vector3 GetAngularVelocity()
		{
			return Controller.angularVelocity;
		}

		#endregion
	}
}