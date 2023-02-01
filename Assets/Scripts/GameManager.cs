using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI topScoreText;

    public string userName;
    public string topScoreName;

    public int currentScore;
    public int topScore;

    public bool isGameOver;
    

    //Start() and Update() methods not used in this code

    private void Awake()
    {
        LoadTopScore();
        topScoreText.text = "Top Score: " + topScoreName + " - " + topScore;
        if (Instance !=null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;        
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
    /// Sets the value of `userName` to the input string `s`.
    /// </summary>
    /// <param name="s">The input string to be assigned to `userName`.</param>
    public void ReadStringInput(string s)
    {
        userName = s;
    }

    

    /// <summary>
    /// Saves the Top Scoring Player to a Json string and file 
    /// </summary>
    
    public void SaveTopScore()
    {
        SaveData saveData = new SaveData();
        saveData.topScoreName = topScoreName;
        saveData.topScore = topScore;
       
        string json = JsonUtility.ToJson(saveData);

        
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    /// <summary>
    /// Loads the saved high score from a json file
    /// </summary>
    public void LoadTopScore()
    {
        // Load the JSON string from the player prefs
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            topScoreName = saveData.topScoreName;
            topScore = saveData.topScore;
            

        }
    }

    /// <summary>
    /// Class to hold data for saving high scores.
    /// </summary>

    [Serializable] public class SaveData
    {
        /// <summary>
        /// The name of the player who achieved the top score.
        /// </summary>
        public string topScoreName;
        /// <summary>
        /// The highest score achieved.
        /// </summary>
        public int topScore;
    }

}
