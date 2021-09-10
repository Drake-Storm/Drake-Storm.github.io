using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboards : MonoBehaviour
{
    public string homeScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Connects to the database to get a list of accounts
    /// </summary>
    public void ConnectToDatabase()
    {

    }

    //create a way to sort the list of accounts


    //Create a way to display the accounts

    public void BackButtonPressed()
    {
        SceneManager.LoadScene(homeScene);
    }
}
