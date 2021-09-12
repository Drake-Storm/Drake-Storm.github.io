using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public string accountScene;
    public string username;
    public string password;
    public InputField usernameInput;
    public InputField passwordInput;
    public Text title;

    /// <summary>
    /// Once the submit button is pressed, get inputs from input fields,
    /// check if an account with that username exists, 
    /// if it does check if the password is correct (signing them in and switching scenes if it is),
    /// if and account with that username does not exist then ask if the user would like to create an account.
    /// </summary>
    public void SubmitPressed()
    {

        //Get strings from inputs
        username = usernameInput.text.ToString();
        password = passwordInput.text.ToString();

        //Checks account database with inputs
        CheckAccount();
        /*bool accountCheck = checkUsername;
        

        if (accountCheck == true)
        {
            //Check password with username
            bool passwordMatch = CheckPassword();

            //If password correct switch scene

            if (passwordMatch)
            {
                Debug.Log("passwordMatch = True: " + checkUsername);
                SwitchScenes();
            }

            else
            {
                passwordInput.placeholder.GetComponent<Text>().text = "Incorrect Password";
                Debug.Log("Incorrect Password");
            }
        }

        else
        {
            Debug.Log("register");
            //Register User
            CallRegistration();
        }*/

    }


    /// <summary>
    /// Checks the database for the username the user entered. 
    /// </summary>
    /// <returns>True if the username already exists in the database, false if it does not.</returns>
    public void CheckAccount()
    {
        
        //Search database for username
        StartCoroutine(UsernameCheck());


    }

    IEnumerator UsernameCheck()
    {
        Debug.Log("IEnumerator Called");
        WWWForm form = new WWWForm();
        form.AddField("username", usernameInput.text.ToString());
        UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost:1080/sqlconnect/checkUsername.php", form);


        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(webRequest.result);
            Debug.Log("Error: " + webRequest.error.ToString());

        }
        else
        {
            Debug.Log("Username checked");
            if (webRequest.downloadHandler.text == "0")
            {
                Debug.Log("checkingTheUsername = true");

                //Username exists, now check the password
                CheckPassword();

            }
            else if (webRequest.downloadHandler.text == "3: username does not exist")
            {
                //Username does not exist, register user
                CallRegistration();
                Debug.Log("Registration called");
            }

            else
            {
                Debug.Log("checkingTheUsername = False");
            }
        }
        //WWW www = new WWW("http://localhost:1080/sqlconnect/checkUsername.php", form);
        //yield return www;
        //if (www.text != "0")
        //{
        //    Debug.Log(www.text);
        //    checkUsername = false;
        //}
        //else
        //{
        //    checkUsername = true;
        //    Debug.Log("checkUsername should be true");
        //}

    }



    /// <summary>
    /// Starts a Coroutine which will check the password to the account
    /// </summary>
    public void CheckPassword()
    {
        StartCoroutine(PasswordCheck());

    }


    IEnumerator PasswordCheck()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameInput.text.ToString());
        form.AddField("password", passwordInput.text.ToString());
        UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost:1080/sqlconnect/checkPassword.php", form);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + webRequest.error.ToString());
        }
        else
        {
            Debug.Log("Password checked");
            if (webRequest.downloadHandler.text == "0")
            {
                Debug.Log("passwordCheck = true");
                SwitchScenes();
            }
            else if (webRequest.downloadHandler.text == "3: Password Incorrect")
            {
                title.text = "Incorrect Password";
                Debug.Log("Incorrect Password");
            }

            else
            {
                Debug.Log(webRequest.downloadHandler.text);

            }
        } 
    }


    /// <summary>
    /// Starts a Coroutine which will register a new player
    /// </summary>
    public void CallRegistration()
    {
        StartCoroutine(Registration());
    }

    /// <summary>
    /// sends username and password to the server to be registered
    /// </summary>
    /// <returns>Errors if there were any, otherwise will just load the account scene.</returns>
    IEnumerator Registration()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameInput.text.ToString());
        form.AddField("password", passwordInput.text.ToString());
        UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost:1080/sqlconnect/register.php", form);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + webRequest.error.ToString());
        }
        else
        {

            Debug.Log("User account Created.");
            SceneManager.LoadScene(accountScene);
        }

    }

    public void SwitchScenes()
    {
        PlayerPrefs.SetString("username", username);
        SceneManager.LoadScene(accountScene);

    }
}
