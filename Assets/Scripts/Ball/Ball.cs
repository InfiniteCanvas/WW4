using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW4.Utility;

public class Ball : MonoBehaviour {

	private void OnCollisionEnter(Collision other)
	{
		IHitable hit = other.gameObject.GetComponent<IHitable>();
		if (hit != null)
		{
			other.gameObject.GetComponent<IHitable>()?.OnHit();
		}
	}
}
