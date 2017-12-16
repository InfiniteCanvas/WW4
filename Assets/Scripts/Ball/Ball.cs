using UnityEngine;
using WW4.Utility;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour, IGrabbable, IPoolable {

	private void OnCollisionEnter(Collision other)
	{
		IHitable hit = other.gameObject.GetComponent<IHitable>();
		if (hit != null)
		{
			other.gameObject.GetComponent<IHitable>().OnHit();
		}
	}

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }
}
