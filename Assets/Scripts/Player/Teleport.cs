using UnityEngine;
using WW4.Utility;

[RequireComponent(typeof(TeleportLaser))]
public class Teleport : MonoBehaviour
{
	private Transform _cameraHead;
	private Transform _cameraRig;
	private InputManager _controller;
	private TeleportLaser _teleportLaser;

	private void Start()
	{
		_teleportLaser = GetComponent<TeleportLaser>();
		_controller = GetComponent<InputManager>();
		_cameraRig = _teleportLaser.CameraRig;
		_cameraHead = _teleportLaser.CameraHead;
	}

	private void Update()
	{
		if (_controller.GetButtonDown(PlayerButtons.Teleport))
			_teleportLaser.TurnOnLaser();
		if (_controller.GetButtonUp(PlayerButtons.Teleport))
		{
			_teleportLaser.TurnOffLaser();

			if (_teleportLaser.CanTeleport)
				TeleportToLaserHitPoint();
		}
	}

	private void TeleportToLaserHitPoint()
	{
		Vector3 difference = _cameraRig.position - _cameraHead.position;
		difference.y = 0;

		_cameraRig.position = _teleportLaser.TeleportPoint + difference;
	}
}