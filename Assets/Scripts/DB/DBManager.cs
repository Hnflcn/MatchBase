using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enums;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Managers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{

    #region Variables In DB

    public static DBManager Instance;
    
    private DatabaseReference levelDB;
    private readonly string firebaseDBurl = "https://puzzle2d-1-default-rtdb.firebaseio.com/";
    

    #endregion
    
    #region Variables In Game

    private UIManager uiManager;

    private bool isFirstGame;
    private string username;
    private int score;
    private int moveCount;
    private int level;
    

    #endregion
    


    #region UnityFunction

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        InitializationInFirebase();
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        FirstInit();
    }
    #endregion
   


    #region Initialization

    private async Task InitializationInFirebase()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

        var logMessage = (dependencyStatus == DependencyStatus.Available)
            ? "Firebase initialized successfully"
            : "Could not resolve all Firebase dependencies: " + dependencyStatus;

        Debug.Log(logMessage);
    }

    
    private async void FirstInit()
    {
        await GetInformation(ConstantVariables.FirstScene, ConstantVariables.FirstGame, ChildType.FirstGame);

        await (isFirstGame
            ? FirstScene()
            : GetInformationAndStartAsync());

        await uiManager.WaitAndStart();
    }

    private async Task FirstScene()
    {
        Debug.Log("first game");
        await SaveInformation<int>(1, ConstantVariables.Users, ConstantVariables.LevelCount, ChildType.Level);
        await SaveInformation<int>(0, ConstantVariables.Users, ConstantVariables.Score, ChildType.Score);
        await SaveInformation<string>("Player", ConstantVariables.Users, ConstantVariables.Username, ChildType.Username);
        await SaveInformation<bool>(false , ConstantVariables.FirstScene,ConstantVariables.FirstGame , ChildType.FirstGame);
    }
    
    private async Task GetInformationAndStartAsync()
    {
        await GetInformation(ConstantVariables.Users, ConstantVariables.Score, ChildType.Score);
        await GetInformation(ConstantVariables.Users, ConstantVariables.Username, ChildType.Username);
        Debug.Log("not first game");
    }

    #endregion
    
    #region DBProcess
    
    private async Task GetInformation(string dbPath, string childKey, ChildType childType)
    {
        levelDB = FirebaseDatabase.GetInstance(firebaseDBurl).GetReference(dbPath);
        var snapshot = await levelDB.GetValueAsync();

        if (snapshot == null)
        {
            Debug.LogError("Error getting level data");
            return;
        }

        var value = snapshot?.Child(childKey).Value;

        var valueDictionary = new Dictionary<ChildType, Action>
        {
            { ChildType.Username, () => username = value.ToString() },
            { ChildType.Level, () => level = (int)(value as long? ?? 0) },
            { ChildType.Score, () => score = (int)(value as long? ?? 0) },
            { ChildType.FirstGame, () => isFirstGame = (bool)(value ?? false) }
        };

        if (valueDictionary.ContainsKey(childType))
        {
            valueDictionary[childType].Invoke();
        }
    }
    
    public async Task SaveInformation<T>(T value, string dbPath, string childKey, ChildType childType)
    {
        levelDB = FirebaseDatabase.GetInstance(firebaseDBurl).GetReference(dbPath);
        await levelDB.Child(childKey).SetValueAsync(value);

        var updateActions = new Dictionary<ChildType, Action<object>>
        {
            { ChildType.Level, newValue => level = Convert.ToInt32(newValue) },
            { ChildType.Score, newValue => score = Convert.ToInt32(newValue) },
            { ChildType.Username, newValue => username = newValue.ToString() },
            { ChildType.MoveCount, newValue => moveCount = Convert.ToInt32(newValue) },
            { ChildType.FirstGame, newValue => isFirstGame = Convert.ToBoolean(newValue) }
        };

        if (updateActions.ContainsKey(childType))
        {
            updateActions[childType](value);
        }
    }


    public int GetLevel()
    {
        return level;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetMoveCount()
    {
        return moveCount;
    }

    public string GetUsername()
    {
        return username;
    }

    #endregion
   
}