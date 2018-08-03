using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gm : MonoBehaviour {

	public static Gm instance = null;

	public float yMinLive = -5.5f;
	public Transform spawnPoint;
	public GameObject playerPrefab;

	PlayerCtrl player;

	public float timeToRespan = 2f;
	
	void Awake() {
		if (instance == null) {
			instance = this;
		}
	}
	
	// Use this for initialization
	void Start () {
		if (player == null) {
			RespwanPlayer();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			GameObject obj = GameObject.FindGameObjectWithTag("Player");
			if (obj != null) {
				player = obj.GetComponent<PlayerCtrl>();
			}
		}
	}
	
	public void RespwanPlayer(){
		Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
	}

	public void KillPlayer() {
		if (player != null) {
			Destroy(player.gameObject);
			Invoke("RespwanPlayer", timeToRespan);
		}
	}
}
