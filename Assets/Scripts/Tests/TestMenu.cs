using System.Collections;
using UnityEngine;
using WW4.Entities;
using WW4.Entities.Book;
using WW4.TestScripts;
using WW4.Utility;

public class TestMenu : MonoBehaviour
{
	private bool _moving, _inRange;
	private GameObject _player;

	public SoundDatabase SoundDatabase;
	public BirdHouse Birdhouse;
	public BirdSpawn BirdSpawn;
	public TeleportLaser TeleportLaser;
	public IdentificationPage IdentificationPage;

	private void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, 200, 100), "Interact with BirdHouse"))
			Birdhouse.Interact(null);
		if (GUI.Button(new Rect(0, Screen.height-100, 200, 100), "Instantiate Bird\nfrom PrefabCatalogue"))
			Instantiate(PrefabCatalogue.Instance["Bird"]);
		if (GUI.Button(new Rect(200, 0, 200, 100), "Move Player in/out of\nBirdSpawn range"))
			TestSpawnPoint();
		if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 100, 200, 100), "Toggle Laser"))
		{
			if (!TeleportLaser.gameObject.activeSelf) TeleportLaser.gameObject.SetActive(true);
			TeleportLaser.ToggleLaser();
		}
		if (GUI.Button(new Rect(200, Screen.height - 100, 200, 100), "Interact with page\nwith random bird"))
			StartCoroutine(InteractWithPage());
		if (GUI.Button(new Rect(400, Screen.height - 100, 200, 100), "Print all identified birds"))
			SoundDatabase.PrintAllIdentifiedBirds();
	}

    private IEnumerator InteractWithPage()
    {
        Bird bird = BirdPool.GetBird();
        yield return new WaitUntil(()=>bird.IsReady);
        IdentificationPage.Interact(bird.gameObject);
    }

    private void TestSpawnPoint()
	{
		if (_player == null)
		{
			_player = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			_player.transform.position = new Vector3(10,10,0);
			_player.tag = "Player";
			_player.AddComponent<Rigidbody>().useGravity = false;
		}

		if (!_moving)
			StartCoroutine(_inRange ? MovePlayer(new Vector3(10, 10, 0)) : MovePlayer(BirdSpawn.transform.position));
	}

	private IEnumerator MovePlayer(Vector3 destination)
	{
		_moving = true;
		for (float i = 0; i < 1; i += Time.deltaTime)
		{
			_player.transform.position = Vector3.Lerp(_player.transform.position, destination, i);
			yield return null;
		}
		_inRange = !_inRange;
		_moving = false;
	}
}