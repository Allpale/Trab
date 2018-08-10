using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gm : MonoBehaviour {

	public static Gm instance = null;

	public float yMinLive = -5.5f;
	
	PlayerCtrl player;
	
	public float timeToRespan = 2f;
	public Transform spawnPoint;
	public GameObject playerPrefab;

	public UI ui;

	GameData data = new GameData();

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
		DisplayHudData();
	}
	
		void DisplayHudData() {
			ui.hud.txtCoinCount.text = "x " + data.coinCount;
		}
		
		public void IncrementCoinCount() {
			data.coinCount++;
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
