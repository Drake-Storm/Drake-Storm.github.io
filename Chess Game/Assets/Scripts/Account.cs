using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Account : MonoBehaviour
{

    public string homeScene;
    public string accountName;
    public Text title;
    public Text compWinsText;
    public Text compLossText;
    public Text compStalemateText;
    public Text twoWinsText;
    public Text twoLossText;
    public Text twoStalemateText;
    private string theWins = " ";
    private string theLosses = " ";
    private string theStalemates = " ";
    private string twoWins = " ";
    private string twoLosses = " ";
    private string twoStalemates = " ";


    // Start is called before the first frame update
    void Start()
    {
        accountName = PlayerPrefs.GetString("username");
        title.text = accountName;

        if (accountName != null)
        {
            StartCoroutine(GetPlayer());


        }
    }

    // Update is called once per frame
    void Update()
    {
        if (theWins != " " && theLosses != " " && theStalemates != " ")
        {
            compWinsText.text = theWins;
            compLossText.text = theLosses;
            compStalemateText.text = theStalemates;
            twoWinsText.text = twoWins;
            twoLossText.text = twoLosses;
            twoStalemateText.text = twoStalemates;
        }
    }

    /// <summary>
    /// Brings the user back to the Main Menu screen when they click the "HOME" button
    /// </summary>
    public void HomeButtonPressed()
    {
        Debug.Log("Home Button Pressed");
        SceneManager.LoadScene(homeScene);
    }

    /// <summary>
    /// This will get the id of the player from the players table of the database, then starts the next coroutine to get their stats.
    /// </summary>
    /// <returns></returns>
    IEnumerator GetPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", accountName);

        UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost:1080/sqlconnect/getPlayer.php", form);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(webRequest.result);
            Debug.Log("Error: " + webRequest.error.ToString());

        }
        else
        {
            string theResult = webRequest.downloadHandler.text;
            if (theResult == "3: username does not exist")
            {
                Debug.Log("Username does not exist");
            }
            else if(theResult == "2: username check failed" || theResult == "1: connection failed")
            {
                Debug.Log("Error: " + theResult);
            }
            else
            {
                GetStats(theResult);
            }
        }
    }

    /// <summary>
    /// This starts the coroutine to get the player stats
    /// </summary>
    /// <param name="id">the id of the player from the players table</param>
    public void GetStats(string id)
    {
        StartCoroutine(GetPlayerStats(id));
    }

    /// <summary>
    /// This will get the player stats and display them
    /// </summary>
    /// <param name="id">the id of the player from the players table</param>
    /// <returns></returns>
    IEnumerator GetPlayerStats(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost:1080/sqlconnect/getPlayerStats.php", form);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(webRequest.result);
            Debug.Log("Error: " + webRequest.error.ToString());

        }
        else
        {
            string theResult = webRequest.downloadHandler.text;
            if (theResult == "3: player does not exist")
            {
                Debug.Log("Username does not exist");
            }
            else if (theResult == "2: player check failed" || theResult == "1: connection failed")
            {
                Debug.Log("Error: " + theResult);
            }
            else
            {
                ThePlayer thePlayer = JsonUtility.FromJson<ThePlayer>(theResult);
                theWins = thePlayer.compwins;
                theLosses = thePlayer.comploss;
                theStalemates = thePlayer.compstalemate;
                twoWins = thePlayer.twowins;
                twoLosses = thePlayer.twoloss;
                twoStalemates = thePlayer.twostalemate;
            }
        }
    }

    /// <summary>
    /// This is used to get the data from the web request
    /// </summary>
    public class ThePlayer
    {
        public string compwins;
        public string comploss;
        public string compstalemate;
        public string twowins;
        public string twoloss;
        public string twostalemate;
    }

}
