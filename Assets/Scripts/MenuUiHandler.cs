using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(1000)]
public class MenuUiHandler : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    [SerializeField] private Player player;
    // Start is called before the first frame update

    /// <summary>
    /// Loads the scene with index 1, which represents the main game scene.
    /// </summary>
    public void StartNew()
    {
        // Check if the player has entered their name
        if (string.IsNullOrEmpty(playerNameInput.text))
        {
            // If the player name is empty, display a warning message
            Debug.LogWarning("Please enter a name before starting a new game.");
        }
        else
        {
            // If the player name is not empty, load the main game scene
            GameObject playerGO = new GameObject("Player");
            player = playerGO.AddComponent<Player>();
            player.Name = playerNameInput.text;
            player.Score = 0;
            GameManager.Instance.SetCurrentPlayer(player);
            GameManager.SetGameOver(false);
            SceneManager.LoadScene(1);
        }
    }

        /// <summary>
        /// Loads the menu scene which is index 0
        /// </summary>
        public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void Exit()
    {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit();
    #endif
    }

}
