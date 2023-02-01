using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text topScoreText;

    // A boolean variable to track the status of the game

    public bool isGameOver;
    private Player _topPlayer;
    private Player _currentPlayer;
    

    //Start() and Update() methods not used in this code

    private void Awake()
    {
        if(Instance !=null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _topPlayer = LoadHighScore();
        
        DontDestroyOnLoad(gameObject);

    }

    /// <summary>
    /// A public static method to set the value of the `isGameOver` variable.
    /// </summary>
    /// <param name="status">The value to set for `isGameOver`</param>
    public static void SetGameOver(bool status)
    {
        // Access the singleton instance of the class and set the value of the `isGameOver` variable
        Instance.isGameOver = status;
    }

    /// <summary>
    /// Sets the current player to the given player object.
    /// </summary>
    /// <param name="player">The player object to set as the current player.</param>
    public void SetCurrentPlayer(Player player)
    {
        _currentPlayer = player;
    }

    /// <summary>
    /// Returns the current player.
    /// </summary>
    /// <returns>The current player.</returns>
    public Player GetCurrentPlayer()
    {
        return _currentPlayer;
    }

    /// <summary>
    /// Returns the Top scoring player.
    /// </summary>
    /// <returns>The Top scoring player.</returns>
    public Player GetTopPlayer()
    {
        return _topPlayer;  
    }

    /// <summary>
    /// Sets the Top scoring player to the given player object.
    /// </summary>
    /// <param name="player">The player to set as the Top player.</param>
    private void SetTopPlayer(Player player)
    {
        _topPlayer = player;
        topScoreText.text = "Top Score: " + _topPlayer.Name + _topPlayer.Score;
    }

    /// <summary>
    /// Saves the Top Scoriing Player to a Json string and file 
    /// </summary>
    
    public void SaveHighScore()
    {
        // Convert the Player object to a JSON string
        string json = JsonUtility.ToJson(_topPlayer);

        // Save the JSON string to the player prefs
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    /// <summary>
    /// Loads the saved high score from a json file
    /// </summary>
    public Player LoadHighScore()
    {
        // Load the JSON string from the player prefs
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SetTopPlayer(JsonUtility.FromJson<Player>(json));

            return _topPlayer;
        }

        else
        {
            return null;
        }
       
    }

    public void CompareScore(Player player)
    {
       int topScore = _topPlayer.Score;
       int curScore = player.Score;
       if(curScore > topScore) 
        {
            SetTopPlayer(player);
        }
       else if(topScore == 0)
        {
            SetTopPlayer(player);
        }
    }
}
