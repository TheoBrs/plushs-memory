using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;

    public virtual void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            Firebase.DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("Firebase is ready to use");
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    public void OnClickSignIn()
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(_email.text, _password.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignIn Canceled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignIn Failed" + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("SignIn Successfully",
                result.User.DisplayName, result.User.UserId);

            GameManager.Instance.IsUserLoggedIn = true;
        });
    }

    public void OnClickSignUp()
    {
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(_email.text, _password.text).ContinueWith(task =>
    {
        if (task.IsCanceled)
        {
            Debug.LogError("SignUp Canceled");
            return;
        }
        if (task.IsFaulted)
        {
            Debug.LogError("SignUp Failed: " + task.Exception.ToString());
            return;
        }

        // Firebase user has been created.
        Firebase.Auth.AuthResult result = task.Result;
        Debug.LogFormat("SignUp Successfully",
            result.User.DisplayName, result.User.UserId);
    });
    }
}