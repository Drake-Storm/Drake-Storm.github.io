using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public string accountScene;
    public string username;
    public string password;
    public InputField usernameInput;
    public InputField passwordInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Once the submit button is pressed, get inputs from input fields,
    /// check if an account with that username exists, 
    /// if it does check if the password is correct (signing them in and switching scenes if it is),
    /// if and account with that username does not exist then ask if the user would like to create an account.
    /// </summary>
    public void submitPressed()
    {

        //Get strings from inputs
        username = usernameInput.text.ToString();
        password = passwordInput.text.ToString();

        //Checks account database with inputs
        bool accountExists = checkAccount(username);

        if (accountExists)
        {
            //Check password with username


            //If password correct switch scene
            switchScenes();
        }

        else
        {
            //Ask user to create account or enter another username

        }

    }

    /// <summary>
    /// Checks the database for the username the user entered. 
    /// </summary>
    /// <param name="username">The first text input the user entered</param>
    /// <returns>True if the username already exists in the database, false if it does not.</returns>
    public bool checkAccount(string username)
    {
        //Search database for username


        return true;
    }

    public void switchScenes()
    {
        SceneManager.LoadScene(accountScene);

    }
}
