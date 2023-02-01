using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private string namePlayer;
    // Start and update not used


    /// <summary>
    /// Creates a new instance of the `Player` class.
    /// </summary>
    /// <param name="namePlayer">The name of the player.</param>
    /// <param name="score">The score of the player.</param>
    public Player(string namePlayer, int score)
    {
        this.namePlayer = namePlayer;
        this.score = score;
    }

    //Getter and Setter for the Players Name
    public string Name
    {
        get => namePlayer;
        set => namePlayer = value;
    }

    //Getter and Setter for the Players Score
    public int Score
    {
        get => score;
        set => score = value;
    }


    /// <summary>
    /// References the current `Player` class.
    /// </summary>
    /// <returns>A reference to the current `Player` class.</returns>
    public Player GetPlayer() 
    {
        return this;
    }


    /// <summary>
    /// Creates a `Player` object from a JSON string representation.
    /// </summary>
    /// <param name="jsonString">The JSON string representation of the `Player` object.</param>
    /// <returns>The `Player` object created from the JSON string.</returns>
    public static Player CreateFromJson(string jsonString)
    {
        return JsonUtility.FromJson<Player>(jsonString);
    }

    /// <summary>
    /// Converts the current object to a JSON string representation.
    /// </summary>
    /// <returns>The JSON string representation of the current object.</returns>
    public string SaveString()
    {
        return JsonUtility.ToJson(this);
    }

    /// <summary>
    /// Adds the specified number of points to the current `Score`.
    /// </summary>
    /// <param name="points">The number of points to add to the `Score`.</param>
    public void AddPoints(int points) 
    {
        var newScore = Score + points;
        Score = newScore;
    }

    /// <summary>
    /// Resets the 'Score' to 0 for a new game
    /// </summary>
    public void ScoreReset()
    {
        Score = 0;
    }
}
