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
            GameManager.Instance.ReadStringInput(playerNameInput.text);
            SceneManager.LoadScene(1);
        }
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
