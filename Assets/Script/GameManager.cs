using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private float spawnRate = 1.0f;
    private int score;
    private int lives;
    private bool paused;

    public bool isGameActive;

    public GameObject titleScene;
    public GameObject pauseScene;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI liveText;

    public Button restartButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangPause();
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficuty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficuty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScene.SetActive(false);
        LiveUpdate(3);
    }
    public void LiveUpdate(int livesToChange)
    {
        lives += livesToChange;
        liveText.text = "Lives: " + lives;
        if(lives <= 0)
        {
            GameOver();
        }
    }
    public void ChangPause()
    {
        if(!paused)
        {
            paused = true;
            pauseScene.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScene.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
