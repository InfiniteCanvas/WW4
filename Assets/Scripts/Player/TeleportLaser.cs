#define TESTING_LASER

using System.Collections;
using UnityEngine;

public class TeleportLaser : MonoBehaviour
{
	private bool _firingLaser;
	private Transform _laserTransform;

	public LayerMask AlphaWalls;

	public bool CanTeleport { get; private set; }
	public Vector3 TeleportPoint { get; private set; }
	public Vector3 TeleportNormal { get; private set; }

	public Transform CameraHead;
	public Transform CameraRig;
	public LineRenderer LaserRenderer;
	public LayerMask TeleportMask;
	public GameObject Reticule;

	private void Start()
	{
		LaserRenderer.enabled = false;
		_laserTransform = LaserRenderer.transform;
		Reticule = Instantiate (Reticule);
		Reticule.SetActive (false);
	}

	public void TurnOnLaser()
	{
		if (!_firingLaser)
		{
			_firingLaser = true;
			StartCoroutine(FireLaser());
		}
	}

	public void TurnOffLaser()
	{
		_firingLaser = false;
	}

	private IEnumerator FireLaser()
	{
		LaserRenderer.enabled = true;
		while (_firingLaser)
		{
			RaycastHit hit;
			Ray ray = new Ray(_laserTransform.position, _laserTransform.forward);

			LaserRenderer.SetPosition(0, ray.origin);

			if (Physics.Raycast (ray, out hit, 100, AlphaWalls)) 
			{
				LaserRenderer.SetPosition (1, ray.GetPoint (100));
				Debug.Log ("Hit alpha wall");
				CanTeleport = false;
				HideReticule ();
			} 
			else 
			{
				if (Physics.Raycast (ray, out hit, 100, TeleportMask)) {								
					LaserRenderer.SetPosition (1, hit.point);
				
					TeleportPoint = hit.point;
					TeleportNormal = hit.normal;

					CanTeleport = true;
					ShowReticule ();
				} else {
					LaserRenderer.SetPosition (1, ray.GetPoint (100));
					CanTeleport = false;
					HideReticule ();
				}
			}

			yield return null;
		}
		LaserRenderer.enabled = false;
	}

	private void ShowReticule()
	{
		Reticule.SetActive(true);
		Reticule.transform.position = TeleportPoint + Vector3.up * .5f;
	}

	private void HideReticule()
	{
		Reticule.SetActive(false);
	}

#if TESTING_LASER

	public void ToggleLaser()
	{
		_firingLaser = !_firingLaser;
		StartCoroutine(FireLaser());
	}

#endif
}