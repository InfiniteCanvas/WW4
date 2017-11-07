#define TESTING_LASER

using System.Collections;
using UnityEngine;
using WW4.Utility;

public class TeleportLaser : MonoBehaviour
{
	private InputManager _controller;
	private Vector3 _teleportPoint;
	private bool _canTeleport;
	private bool _firingLaser;
	private Transform _laserTransform;

	public Transform CameraHead;
	public Transform CameraRig;
	public LineRenderer LaserRenderer;
	public LayerMask TeleportMask;

	private void Start()
	{
		LaserRenderer.enabled = false;
		_laserTransform = LaserRenderer.transform;
		_controller = GetComponent<InputManager>();
	}

	private void Update()
	{
		if (_controller.GetButtonDown(PlayerButtons.Teleport))
			if (!_firingLaser)
			{
				_firingLaser = true;
				StartCoroutine(FireLaser());
			}

		if (_controller.GetButtonUp(PlayerButtons.Teleport))
		{
			_firingLaser = false;
			TryTeleport();
		}
	}

	private IEnumerator FireLaser()
	{
		LaserRenderer.enabled = true;
		while (_firingLaser)
		{
			RaycastHit hit;
			Ray ray = new Ray(_laserTransform.position, _laserTransform.forward);

			LaserRenderer.SetPosition(0, ray.origin);
			if (Physics.Raycast(ray, out hit, 100, TeleportMask))
			{
				LaserRenderer.SetPosition(1, hit.point);
				_teleportPoint = hit.point;
				_canTeleport = true;
			}
			else
			{
				LaserRenderer.SetPosition(1, ray.GetPoint(100));
				_canTeleport = false;
			}

			yield return null;
		}
		LaserRenderer.enabled = false;
	}

	private void TryTeleport()
	{
		if (!_canTeleport) return;

		Vector3 offset = CameraRig.position - CameraHead.position;
		offset.y = 0;

		CameraRig.position = _teleportPoint + offset;
	}

#if TESTING_LASER

	public void ToggleLaser()
	{
		_firingLaser = !_firingLaser;
		StartCoroutine(FireLaser());
	}

#endif
}