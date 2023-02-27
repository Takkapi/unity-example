using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using System.ComponentModel.Design;
using System.Threading;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text waveText;
    [SerializeField] GameObject howToPlayObject;

    private SpawnEnemyManager spawnManager;
    private WaveManager wm;
    [SerializeField] BasicEnemyWaveSpawner waveManager;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject difficultyText;
    [SerializeField] PlayerController player;
    public int score;
    public int highscore;
    public int wave;
    public int deaths;
    public bool gameOver = false;
    public bool preStart = true;

    [Space]
    [Header("Death Screen")]
    [SerializeField] GameObject deathScreen;
    [SerializeField] TMP_Text wavesText;
    [SerializeField] TMP_Text scoreText;

    [Header("Enemy Prefabs")]

    [SerializeField] GameObject basicEnemy;
    [SerializeField] GameObject fastEnemy;
    [SerializeField] GameObject smartEnemy;
    

    void Start()
    {
        string path = Application.persistentDataPath + "/takk.api";
        PlayerData data = SaveFiles.LoadPlayer();

        if(File.Exists(path)) { 
            deaths = data.deaths;
            highscore = data.highscore;
        }

        deaths++;

        difficultyText.SetActive(false);
        healthBar.SetActive(false);
        deathScreen.SetActive(false);
        howToPlayObject.SetActive(true);
    }

    void Update()
    {
        if(player.health <= 0) 
        {
            gameOver = true;
        }

        if (gameOver)
        {
            wavesText.SetText("Waves played: " + wave.ToString());
            scoreText.SetText("Score: " + score.ToString());
            deathScreen.SetActive(true);
            healthBar.SetActive(false);
            difficultyText.SetActive(false);
            SaveFiles.SavePlayer(this);
            if(score > highscore) {
                highscore = score;
                SaveFiles.SavePlayer(this);
            }
        }

        // WAVES AND SCORE CONTROLLER
        if(preStart == false)
        {
            healthBar.SetActive(true);
            difficultyText.SetActive(true);
            if(!gameOver)
                score++;
        }

        waveText.SetText(wave.ToString());

        if (score == 300)
        {
            //Wave 1
            wave += 1;
            Instantiate(basicEnemy);
            //waveManager.SpawnWave();

            //spawnManager.SpawnBasicEnemy(8, 10);
        }
        if(score == 2500)
        {
            //Wave 2
            wave += 1;
            Instantiate(basicEnemy);
            //spawnManager.SpawnBasicEnemy(8, 10);
        }
        if(score == 4500)
        {
            //Wave 3
            wave++;
            Instantiate(basicEnemy);
            //spawnManager.SpawnBasicEnemy(8, 10);
        }
        if(score == 6500)
        {
            //Wave 4
            wave++;
            Instantiate(basicEnemy);
        }
        if(score == 8500)
        {
            //Wave 5
            wave++;
            Instantiate(fastEnemy);
        }
        if(score == 10500)
        {
            //Wave 6
            wave++;
            Instantiate(fastEnemy);
        }
        if(score == 12500)
        {
            //Wave 7
            wave++;
            waveManager.SpawnWave();
        }
        if (score == 14500)
        {
            //Wave 8
            wave++;
            Instantiate(fastEnemy);
        }
        if (score == 16500)
        {
            //Wave 9
            wave++;
            Instantiate(basicEnemy);
        }
        if(score == 18500) {
            waveManager.SpawnWave();
            Thread.Sleep(500);
            waveManager.SpawnWave();
        }
    }
}
