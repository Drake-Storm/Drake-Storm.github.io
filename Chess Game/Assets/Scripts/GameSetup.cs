using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{

    public string gameScene;
    public string opponent;
    public string colour;
    public TMPro.TMP_Dropdown opponentDD;
    public TMPro.TMP_Dropdown colourDD;

    

    /// <summary>
    /// function called when the user clicks the submit button
    /// </summary>
    public void SubmitBtnPressed()
    {
        SceneManager.LoadScene(gameScene);
    }

    /// <summary>
    /// gets the user's choice of opponent
    /// </summary>
    /// <param name="sender">the dropdown input</param>
    public void DropDownOpponentChanged(TMPro.TMP_Dropdown sender)
    {
        opponent = sender.options[sender.value].text;
        Debug.Log("opponent: " + opponent);
    }

    /// <summary>
    /// gets the user's choice of colour, determining the player turns
    /// </summary>
    /// <param name="sender">the dropdown input</param>
    public void DropDownColourChanged(TMPro.TMP_Dropdown sender)
    {
        colour = sender.options[sender.value].text;
        Debug.Log("colour: " + colour);
    }

    /// <summary>
    /// saves user choices in PlayerPrefs for the next scene
    /// </summary>
    private void OnDisable()
    {
        PlayerPrefs.SetString("opponent", opponent);
        PlayerPrefs.SetString("colour", colour);

    }
}
