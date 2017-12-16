using System.Collections;
using UnityEngine;
using WW4.Utility;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour, IGrabbable, IPoolable
{
    public Rigidbody Rigidbody { get; private set; }
    private Coroutine _delayedDespawn;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();        
    }

	private void OnCollisionEnter(Collision other)
	{
		IHitable hit = other.gameObject.GetComponent<IHitable>();
		if (hit != null)
		{
			other.gameObject.GetComponent<IHitable>().OnHit();
		}
		else
		{
            if(_delayedDespawn==null)
                _delayedDespawn = StartCoroutine(DelayedDespawn(1f));
        }
	}

    private IEnumerator DelayedDespawn(float d)
    {
        yield return new WaitForSeconds(d);
        PrefabPool.DespawnClone(gameObject);
    }

    public void Spawn()
    {
        _delayedDespawn = null;
        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        StopCoroutine(_delayedDespawn);
        gameObject.SetActive(false);
    }
}
