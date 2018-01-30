using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTeleportCircle : MonoBehaviour {

	void Update () {
		transform.RotateAround (transform.position, transform.up, 36*Time.deltaTime);
	}
}
