using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		} else {
			Debug.Log("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Boundary")) {
			return;
		}
		if (explosion != null) {
			Instantiate (explosion, transform.position, transform.rotation);
		}
		if (other.gameObject.CompareTag ("Earth")) {
			gameController.DestroyedEarth ();
			Destroy (this.gameObject);
			return;
		}
		if (other.gameObject.CompareTag ("House")) {
			gameController.DestroyedHouse ();
		}
		if (other.gameObject.CompareTag ("ExplosionObj")) {
			Destroy (this.gameObject);
		}
		gameController.addScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (this.gameObject);

	}
}
