using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text scoreText;
    public Text topScoreText;
    public GameObject gameOver;
    
    private bool _isGameStarted = false;
    private int _gamePoints;
           
    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.GetCurrentPlayer().ScoreReset();
        
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
        Player topPlayer = GameManager.Instance.GetTopPlayer();
        int topScoreCheck = topPlayer.Score;
        Player currentPlayer = GameManager.Instance.GetCurrentPlayer();
        if (topScoreCheck == 0)
        {
            topPlayer = currentPlayer;
        }
        topScoreText.text = "Top Score: " + topPlayer.Name + " - " + topPlayer.Score;
    }

    private void Update()
    {
        if (!_isGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.SetGameOver(false);
                _isGameStarted = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (GameManager.Instance.isGameOver)
        {
            gameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void AddPoint(int point)
    {
        _gamePoints += point;
        scoreText.text = $"Score : {_gamePoints}";
        GameManager.Instance.GetCurrentPlayer().AddPoints(point);
    }

}
