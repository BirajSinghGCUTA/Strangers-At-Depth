using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using Firebase.Database;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FirebaseManager : MonoBehaviour
{
    //p class NewBehaviourScript : MonoBehaviour
    //Firebase variables
    //public Text test;
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    //swaa
    void Awake()
    {
        //Load pop up ad team 12
        //SceneManager.LoadScene("Advertisement", LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Login"));

        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
                
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
            /*readtest();
            displayKrakens();*/

            yield return new WaitForSeconds(1);

            //UIManager.instance.UserDataScreen(); change scene

            confirmLoginText.text = "";
            SceneManager.LoadScene("HomeScreenScene");
        }
    }





    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username!";
        }

        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }

        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);



            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);

                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);
                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                    }
                    //put here if there is User
                    var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);
                    yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
                    if (DBTask.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
                    }
                    else
                    {
                        //update username in database
                    }

                    int totalWins = 0;
                    int totalKrakens = 0;
                    int skinsOwned = 0;
                    string dailyReward = "0";
                    var DBInitial = DBreference.Child("users").Child(User.UserId).Child("Total Wins").SetValueAsync(totalWins);
                    yield return new WaitUntil(predicate: () => DBInitial.IsCompleted);
                    if (DBInitial.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register total wins task with {DBInitial.Exception}");
                    }
                    else
                    {
                        //make initial wins 0
                    }
                    var DBNext = DBreference.Child("users").Child(User.UserId).Child("Total Krakens").SetValueAsync(totalKrakens);
                    yield return new WaitUntil(predicate: () => DBNext.IsCompleted);
                    if (DBNext.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register total krakens task with {DBNext.Exception}");
                    }
                    else
                    {
                        //make initial wins 0
                    }
                    var DBSkin = DBreference.Child("users").Child(User.UserId).Child("Skins Owned").SetValueAsync(skinsOwned);
                    yield return new WaitUntil(predicate: () => DBSkin.IsCompleted);
                    if (DBSkin.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register skin owned task with {DBSkin.Exception}");
                    }
                    else
                    {
                        //make skin owned true
                    }
                    var DBDaily = DBreference.Child("users").Child(User.UserId).Child("Daily Reward").SetValueAsync(dailyReward);
                    yield return new WaitUntil(predicate: () => DBDaily.IsCompleted);
                    //test to write
                    //var DBDailyWrite = DBreference.Child("users").Child(User.UserId).Child("DailyReward").SetValueAsync("6 million");
                    if (DBDaily.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register dailyReward owned task with {DBDaily.Exception}");
                    }
                    else
                    {
                        //make skin owned true
                    }
                }
            }
        }

    }
    private IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };

        //Call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = User.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated
        }
    }

    
    public int time = 0;

    //Use fixed update beacuase its called every fixed framerate frame
    void FixedUpdate()
    {

        if (!Input.anyKey)
        {

            //Starts counting when no button is being pressed
            time = time + 1;
        }
        else
        {

            // If a button is being pressed restart counter to Zero
            time = 0;
        }

        //Now after n frames of nothing being pressed it will do activate this if statement
        //50 frames per second
        //750 = 15 secs
        if (time == 500)
        {
            //animator.SetTrigger("pop");
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Advertisement"));
            Debug.Log("500 frames passed with no input");
            SceneManager.LoadScene("Advertisement", LoadSceneMode.Additive);

            //Now you could set time too zero so this happens every 100 frames
            //time = 0;
        }

    }
}



    /*void readtest()
    {
        DBreference.Child("users").Child(User.UserId).Child("Total Krakens").SetValueAsync(500);
    }

    void displayKrakens()
    {
        DBreference.Child("users").Child(User.UserId).Child("Total Krakens").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                //Debug.Log("Hello");
                DataSnapshot snapshot = task.Result;
                //test.text = snapshot.Value.ToString();
                //Debug.Log(test.text);
                foreach (DataSnapshot user in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)user.Value;
                    Debug.Log("" + dictUser["Total Krakens"]);
                }

            }
            else
            {
                //krakensHeld.text = "ERROR";
                Debug.Log("Error getting krakens");
            }
        });
    }*/

