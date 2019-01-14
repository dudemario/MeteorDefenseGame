using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public GameObject house;
	public Material[] materials;
	public GameObject plane;
	private Vector3 spawnValues = new Vector3(6, 0, 6);
	//public int hazardCount;
	private float spawnWait = 1;
	private float startWait = 5;
	private float waveWait = 3;
	public GUIText[] texts; //[scoreText, gameOverText, restartText]
	private bool gameOver;
	//private bool restart;
	private int score;
	private int numDestroyedHouses = 0;
	private int numReached = 0;
	private int totalHouses = 8;
	private int waveCount;
	private float superSpeed = 1;

	void Start() {
		gameOver = false;
		//restart = false;
		texts[2].text = "";
		texts[1].text = "Left/Right or A/D to change shooters\nMouse Left button to fire\n1 to Restart, Space to increase speed\nT to skip 5 waves\n\nDeath when all houses are destroyed\nor 20 meteors hit Earth";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
		CreateHouses ();
	}

	void CreateHouses() {
		for (float i = -6.5f; i < 7.5f; i+=4) {
			Instantiate(house, new Vector3(i, 0, -4.8f), Quaternion.identity);
			Instantiate(house, new Vector3(i+1, 0, -4.8f), Quaternion.identity);
		}
	}

	void Update() {
		if (Input.GetButtonDown("Speed Up")) {
			waveWait = 0;
			superSpeed += 0.8f;
		}
		if (Input.GetButtonDown("Restart")) {
			SceneManager.LoadScene (0);
		}
		if (Input.GetButtonDown("Skip")) {
			waveCount += 5;
		}
		if (Input.GetButtonDown("Pause")) {
			gameOver = true;
		}
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds (startWait);
		texts [1].text = "";
		while (true) {
			for (int i = 0; i < waveCount; i++) {
				SpawnOne ();
				yield return new WaitForSeconds (spawnWait);
			}
			waveCount += 1;
			//Debug.Log ((waveCount / 5) % 2);
			plane.GetComponent<Renderer> ().material= materials [(waveCount / 5) % 2];
			yield return new WaitForSeconds (waveWait);
			if (gameOver) {
				texts[2].text = "Press '1' to Restart";
				//restart = true;
				break;
			}
		}
	}
	void SpawnOne() {
		GameObject hazard = hazards [Random.Range (0, hazards.Length)];
		Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
		Quaternion spawnRotation = Quaternion.identity;
		GameObject spawn = Instantiate (hazard, spawnPosition, spawnRotation);
		Rigidbody rb = spawn.GetComponent<Rigidbody> ();
		rb.velocity = new Vector3 (Random.Range (-2, 2), 0, -2)*((waveCount/10)+1*superSpeed);
	}

	public void addScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore() {
		texts[0].text = "Score: " + score;
	}
	public void GameOver(){
		texts[1].text = "Game Over!\nWaves Completed: "+waveCount;
		gameOver = true;
	}

	public void DestroyedHouse() {
		numDestroyedHouses += 1;
		if (numDestroyedHouses == totalHouses) {
			GameOver();
		}
	}

	public void DestroyedEarth() {
		numReached += 1;
		if (numReached == 20) {
			GameOver();
		}
	}
}
