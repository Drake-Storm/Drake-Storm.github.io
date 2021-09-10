using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string setupScene;
    public string accountScene;
    public string leaderboardScene;


    // Start is called before the first frame update
    void Start()
    {
        
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
        SceneManager.LoadScene(setupScene);
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
