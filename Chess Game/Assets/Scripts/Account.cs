using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Account : MonoBehaviour
{

    public string homeScene;
    public string accountName;
    public string wins;
    public string losses;
    public string stalemates;
    public Text title;
    public Text winsText;
    public Text lossText;
    public Text stalemateText;

    // Start is called before the first frame update
    void Start()
    {
        accountName = PlayerPrefs.GetString("username");
        title.text = accountName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Brings the user back to the Main Menu screen when they click the "HOME" button
    /// </summary>
    public void HomeButtonPressed()
    {
        SceneManager.LoadScene(homeScene);
    }

    /// <summary>
    /// Gets the number of wins for the account
    /// </summary>
    /// <param name="account">subject to change when actually building database</param>
    /// <returns>number of wins</returns>
    public int GetWins(string account)
    {
        int wins = 0;
        return wins;
    }

    /// <summary>
    /// Gets the number of losses for the account
    /// </summary>
    /// <param name="account">Subject to change</param>
    /// <returns>Number of losses</returns>
    public int GetLoss(string account)
    {
        int loss = 0;
        return loss;
    }

    /// <summary>
    /// Gets the number of stalemates for the account
    /// </summary>
    /// <param name="account">subject to change</param>
    /// <returns>Number of stalemates</returns>
    public int GetStalemate(string account)
    {
        int stalemate = 0;
        return stalemate;
    }


}
