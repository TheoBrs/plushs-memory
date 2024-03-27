using Firebase.Database;
using TMPro;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField Name;
    [SerializeField] private TMP_InputField Gold;

    private string userID;
    private DatabaseReference dbReference;

    private void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser()
    {
        User newUser = new(Name.text, int.Parse(Gold.text));
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }
}
