using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Leaderboards : MonoBehaviour
{
    public string homeScene;
    public Account[] thePlayers;

    public Text winsText1;
    public Text winsText2;
    public Text winsText3;
    public Text lossText1;
    public Text lossText2;
    public Text lossText3;
    public Text stalemateText1;
    public Text stalemateText2;
    public Text stalemateText3;
    public Text nameText1;
    public Text nameText2;
    public Text nameText3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetPlayers());
    }

    /// <summary>
    /// Connects to the database to get a list of accounts
    /// </summary>
    IEnumerator GetPlayers()
    {
        WWWForm form = new WWWForm();
        UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost:1080/sqlconnect/getAllPlayers.php", form);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(webRequest.result);
            Debug.Log("Error: " + webRequest.error.ToString());

        }
        else
        {
            string theResult = webRequest.downloadHandler.text;
            if (theResult == "5: player does not exist")
            {
                Debug.Log("Player does not exist");
            }
            else if (theResult == "3: no players found")
            {
                Debug.Log("No players found");
            }
            else if (theResult == "4: player check failed" || theResult == "1: connection failed" || theResult == "2: players check failed")
            {
                Debug.Log("Error: " + theResult);
            }
            else
            {

                thePlayers = JsonHelper.getJsonArray<Account>(theResult);

                winsText1.text = thePlayers[0].wins;
                lossText1.text = thePlayers[0].loss;
                stalemateText1.text = thePlayers[0].stalemate;
                nameText1.text = thePlayers[0].name;

                if(thePlayers.Length > 1)
                {
                    winsText2.text = thePlayers[1].wins;
                    lossText2.text = thePlayers[1].loss;
                    stalemateText2.text = thePlayers[1].stalemate;
                    nameText2.text = thePlayers[1].name;

                    if (thePlayers.Length > 2)
                    {
                        winsText3.text = thePlayers[2].wins;
                        lossText3.text = thePlayers[2].loss;
                        stalemateText3.text = thePlayers[2].stalemate;
                        nameText3.text = thePlayers[2].name;
                    }
                }
            }
        }
    }

    /// <summary>
    /// This will sort the accounts by # of wins, then update the screen
    /// </summary>
    public void SortPlayersByWin()
    {
        Array.Sort(thePlayers, delegate (Account x, Account y) { return x.wins.CompareTo(y.wins); });
        Array.Reverse(thePlayers);
        winsText1.text = thePlayers[0].wins;
        lossText1.text = thePlayers[0].loss;
        stalemateText1.text = thePlayers[0].stalemate;
        nameText1.text = thePlayers[0].name;

        if(thePlayers.Length > 1)
        {
            winsText2.text = thePlayers[1].wins;
            lossText2.text = thePlayers[1].loss;
            stalemateText2.text = thePlayers[1].stalemate;
            nameText2.text = thePlayers[1].name;

            if(thePlayers.Length > 2)
            {

                winsText3.text = thePlayers[2].wins;
                lossText3.text = thePlayers[2].loss;
                stalemateText3.text = thePlayers[2].stalemate;
                nameText3.text = thePlayers[2].name;
            }
        }

    }
    /// <summary>
    /// This will sort the accounts by # of losses, then update the screen
    /// </summary>
    public void SortPlayersByLoss()
    {
        Array.Sort(thePlayers, delegate (Account x, Account y) { return x.loss.CompareTo(y.loss); });
        Array.Reverse(thePlayers);
        winsText1.text = thePlayers[0].wins;
        lossText1.text = thePlayers[0].loss;
        stalemateText1.text = thePlayers[0].stalemate;
        nameText1.text = thePlayers[0].name;

        if (thePlayers.Length > 1)
        {
            winsText2.text = thePlayers[1].wins;
            lossText2.text = thePlayers[1].loss;
            stalemateText2.text = thePlayers[1].stalemate;
            nameText2.text = thePlayers[1].name;

            if (thePlayers.Length > 2)
            {

                winsText3.text = thePlayers[2].wins;
                lossText3.text = thePlayers[2].loss;
                stalemateText3.text = thePlayers[2].stalemate;
                nameText3.text = thePlayers[2].name;
            }
        }
    }
    /// <summary>
    /// This will sort the accounts by # of stalemates, then update the screen
    /// </summary>
    public void SortPlayersByStalemate()
    {
        Array.Sort(thePlayers, delegate (Account x, Account y) { return x.stalemate.CompareTo(y.stalemate); });
        Array.Reverse(thePlayers);
        winsText1.text = thePlayers[0].wins;
        lossText1.text = thePlayers[0].loss;
        stalemateText1.text = thePlayers[0].stalemate;
        nameText1.text = thePlayers[0].name;

        if (thePlayers.Length > 1)
        {
            winsText2.text = thePlayers[1].wins;
            lossText2.text = thePlayers[1].loss;
            stalemateText2.text = thePlayers[1].stalemate;
            nameText2.text = thePlayers[1].name;

            if (thePlayers.Length > 2)
            {

                winsText3.text = thePlayers[2].wins;
                lossText3.text = thePlayers[2].loss;
                stalemateText3.text = thePlayers[2].stalemate;
                nameText3.text = thePlayers[2].name;
            }
        }
    }
    /// <summary>
    /// This will sort the accounts by name, then update the screen
    /// </summary>
    public void SortPlayersByName()
    {
        Array.Sort(thePlayers, delegate (Account x, Account y) { return x.name.CompareTo(y.name); });
        winsText1.text = thePlayers[0].wins;
        lossText1.text = thePlayers[0].loss;
        stalemateText1.text = thePlayers[0].stalemate;
        nameText1.text = thePlayers[0].name;

        winsText2.text = thePlayers[1].wins;
        lossText2.text = thePlayers[1].loss;
        stalemateText2.text = thePlayers[1].stalemate;
        nameText2.text = thePlayers[1].name;

        winsText3.text = thePlayers[2].wins;
        lossText3.text = thePlayers[2].loss;
        stalemateText3.text = thePlayers[2].stalemate;
        nameText3.text = thePlayers[2].name;
    }


    public void BackButtonPressed()
    {
        SceneManager.LoadScene(homeScene);
    }

    /// <summary>
    /// This is used to get the data from the web request
    /// </summary>
    [Serializable]
    public class Account
    {
        public string name;
        public string wins;
        public string loss;
        public string stalemate;
    }

    /// <summary>
    /// This is to get a list of accounts from the json array gotten from the php scripts
    /// logic from: https://forum.unity.com/threads/how-to-load-an-array-with-jsonutility.375735/
    /// </summary>
    [Serializable]
    public class JsonHelper
    {
        public static T[] getJsonArray<T>(string json)
        {
            string newJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] array;
        }
    }
}
