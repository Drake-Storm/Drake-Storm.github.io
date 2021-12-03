using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text titleText;
    public string setupScene;
    public string accountScene;
    public string leaderboardScene;
    private int justLaunched;


    // Start is called before the first frame update
    void Start()
    {
        justLaunched = PlayerPrefs.GetInt("justLaunched");
        if (justLaunched == 0)
        {
            PlayerPrefs.SetInt("justLaunched", 1);
            PlayerPrefs.DeleteKey("username");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// function called when the user clicks the Play button
    /// </summary>
    public void PlayBtnPressed()
    {
        if(PlayerPrefs.HasKey("username"))
        {
            SceneManager.LoadScene(setupScene);
        }
        else
        {
            titleText.text = "sign in to play";
        }
    }

    /// <summary>
    /// function called when the user clicks the account button
    /// </summary>
    public void AccountBtnPressed()
    {
        SceneManager.LoadScene(accountScene);
    }

    /// <summary>
    /// function called when the user clicks the leaderboard button
    /// </summary>
    public void LeaderboardBtnPressed()
    {
        SceneManager.LoadScene(leaderboardScene);
    }

}
