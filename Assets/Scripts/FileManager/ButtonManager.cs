using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public string path;
    private static string GetFilePath(string FolderName, string FileName = "")
    {
        string filePath;
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        // mac
        filePath = Path.Combine(Application.streamingAssetsPath, ("data/" + FolderName));

        if (FileName != "")
            filePath = Path.Combine(filePath, (FileName + ".txt"));
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        // windows
        filePath = Path.Combine(Application.persistentDataPath, ("data/" + FolderName));

        if (FileName != "")
            filePath = Path.Combine(filePath, (FileName + ".json"));
#elif UNITY_ANDROID
        // android
        filePath = Path.Combine(Application.persistentDataPath, ("data/" + FolderName));

        if(FileName != "")
            filePath = Path.Combine(filePath, (FileName + ".json"));
#elif UNITY_IOS
        // ios
        filePath = Path.Combine(Application.persistentDataPath, ("data/" + FolderName));

        if(FileName != "")
            filePath = Path.Combine(filePath, (FileName + ".txt"));
#endif
        return filePath;
    }
    public void OnBtnSaveClick()
    {
        string filePath = "";
        filePath = GetFilePath("data", "playerData");
        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        }
        Debug.Log(filePath);
        GameObject Player = GameObject.Find("Hero");
        PlayerEntity playerEntity = Player.GetComponent<PlayerEntity>();
        PlayerModel playerModel = new PlayerModel();
        playerModel.lv = playerEntity.Level;
        playerModel.currentExp = playerEntity.CurrentExp;
        playerModel.currentHp = playerEntity.CurrentHp;
        playerModel.position = Player.transform.position;
        string playerJson = JsonUtility.ToJson(playerModel, true);
        // Player 
        File.WriteAllText(filePath, playerJson);
    }
    
    

    public void OnBtnLoadClick()
    {
        PlayerPrefs.SetInt("isLoad", 1);
        Debug.Log(PlayerPrefs.GetInt("isLoad"));
        SceneManager.LoadScene("GameScene");
    }

    public void OnBtnStartClick()
    {
        PlayerPrefs.SetInt("isLoad", 0);
        SceneManager.LoadScene("GameScene");
    }

    public void OnBtnReturnClick()
    {
        Debug.Log(12);
        SceneManager.LoadScene("TestLoadScene");
    }
}
