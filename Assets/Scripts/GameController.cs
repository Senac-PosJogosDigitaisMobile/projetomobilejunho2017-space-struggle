using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public GameObject player;
	public GameObject playerRespawn;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public int enemyCount;

	public Text waveTXT;
	public Text scoreTXT;
	public Text title;
	public Text gameOverTXT;
	public Text start;
    public Text highScoreTXT;
	private int score;
    private int highScore;
	private int waveCount;
	private int enemyIniCount;
	private int waveLength;

    public bool gameOver;
	private bool restart;
	private bool firstTime;

	private AudioSource audio;


	void Start() {
		audio = GetComponent<AudioSource> ();
		audio.mute = true;
		Debug.Log ("Iniciando");
		waveLength = 10;
		highScore = 0;
        UpdateHighScore();
        gameOverTXT.enabled = false;
		waveTXT.enabled = false;
		firstTime = true;
		CleanIni ();

	}

	void Update () {

		if (firstTime && Input.anyKeyDown) {
			firstTime = false;
			title.enabled = false;
			start.enabled = false;
			audio.mute = false;
			StartCoroutine (SpawnWaves ());

		} 

		if (restart) {
			start.enabled = true;
			if (Input.anyKeyDown) {
				//SceneManager.LoadScene ("Main");
				start.enabled = false;
				gameOverTXT.enabled = false;
				CleanIni ();
				audio.mute = false;
				StartCoroutine (SpawnWaves ());
			}
		}

	}
		

	IEnumerator SpawnWaves(){
		
		while (true) {

			if (gameOver) {
				restart = true;
				break;
			} else {
				waveTXT.text = "WAVE " + waveCount;
				waveTXT.enabled = true;
				yield return new WaitForSeconds (waveWait);
				waveTXT.enabled = false;

				for (int j = 0; j < waveLength; j++) {

					for (int i = 0; i < enemyIniCount; i++) {
					
						GameObject hazard = hazards [Random.Range (0, enemyIniCount)];
						Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (hazard, spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);

					}

				}

				if (enemyIniCount < hazards.Length) {
					enemyIniCount++;
				}
				waveCount++;
				yield return new WaitForSeconds (waveWait);

			}
		}
	}

	public void AddScore (int newScoreValue){

		score += newScoreValue;
		UpdateScore ();

	}

	void UpdateScore(){

		scoreTXT.text = score.ToString();

	}

    void UpdateHighScore()
    {

        highScoreTXT.text = highScore.ToString();

    }



    public void GameOver() {

        if (score > highScore) {

			highScore = score;
            UpdateHighScore();

        }

		audio.mute = true;
		gameOverTXT.enabled = true;
		gameOver = true;
	}

	public void CleanIni(){
		restart = false;
		gameOver = false;
		score = 0;
		waveCount = 1;
		UpdateScore ();
		enemyIniCount = enemyCount ;
		Instantiate (player, playerRespawn.transform.position, playerRespawn.transform.rotation);
	}




}
