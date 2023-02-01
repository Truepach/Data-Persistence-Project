using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text scoreText;
    public Text topScoreText;
    public GameObject gameOver;
    
    private bool m_Started = false;
    private int m_points;

    private bool m_GameOver = false;
           
    // Start is called before the first frame update
    private void Start()
    {
         
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        topScoreText.text = $"Top Score: {GameManager.Instance.topScoreName} {GameManager.Instance.topScore}";
        Debug.Log("Current Name: " + GameManager.Instance.userName);
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               // GameManager.SetGameOver(false);
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            gameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void AddPoint(int point)
    {
        m_points += point;
        scoreText.text = $"Score : {m_points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        gameOver.SetActive(true);
        if(m_points > GameManager.Instance.topScore) 
        {
            GameManager.Instance.topScore = m_points;
            if (!string.IsNullOrEmpty(GameManager.Instance.userName))
            {
                string currentUser = GameManager.Instance.userName;
                GameManager.Instance.topScoreName = currentUser;
            }
            else
            {
                GameManager.Instance.topScoreName = "Unknown Player";
            }
            topScoreText.text = $"Top Score: {GameManager.Instance.topScoreName} {GameManager.Instance.topScore}";
            GameManager.Instance.SaveTopScore();


        } 
    }
}
