using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gm : MonoBehaviour {

	public static Gm instance = null;

	public float yMinLive = -5.5f;
	
	PlayerCtrl player;
	
	public float timeToRespan = 2f;
	public Transform spawnPoint;
	public GameObject playerPrefab;

	public float maxTime = 120f;
	bool timerOn = true;
	float timeLeft;

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
		timeLeft = maxTime;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (player == null) {
			GameObject obj = GameObject.FindGameObjectWithTag("Player");
			if (obj != null) {
				player = obj.GetComponent<PlayerCtrl>();
			}
		}
		UpdateTimer();
		DisplayHudData();
	}
	
	public void RestartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ExitToMainMenu() {
		LoadScene("MainMenu");
	}

	public void CloseApp() {
		Application.Quit();
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}
	
	void UpdateTimer() {
		if (timerOn) {
			timeLeft = timeLeft - Time.deltaTime;
			if (timeLeft <= 0f) {
				timeLeft = 0;
				ExpirePlayer();
			}
		}
	}
		
	void DisplayHudData() {
			ui.hud.txtCoinCount.text = "x " + data.coinCount;
			ui.hud.txtLifeCount.text = "x " + data.lifeCount;
			ui.hud.txtTimer.text = "Timer: " + timeLeft.ToString("F0");
	}
		
	public void IncrementCoinCount() {
			data.coinCount++;
	}

	public void DecrementLives() {
		data.lifeCount--;
	}

	public void KillPlayer() {
		if (player != null) {
			Destroy(player.gameObject);
			DecrementLives();
			if (data.lifeCount > 0) {
			Invoke("RespwanPlayer", timeToRespan);
			}		
		else {
			GameOver();
			}
		}
	}
	public void ExpirePlayer() {
		if (player != null) {
			Destroy(player.gameObject);			
		}
		GameOver();
	}

	void GameOver() {
		timerOn = false;
		ui.gameOver.txtCoinCount.text = "Coins: " + data.coinCount;
		ui.gameOver.txtTimer.text = "Timer: " + timeLeft.ToString("F0");
		ui.gameOver.gameOverPanel.SetActive(true);
	}

	public void RespwanPlayer(){
		Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
		}
	public void LevelComplete() {
		Destroy(player.gameObject);
		timerOn = false;
		ui.levelComplete.txtCoinCount.text = "Coins: " + data.coinCount;
		ui.levelComplete.txtTimer.text = "Timer: " + timeLeft.ToString("F0");
		ui.levelComplete.levelCompletePanel.SetActive(true);	
	}
}