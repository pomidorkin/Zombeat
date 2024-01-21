using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int coins = 0;
    public List<WeaponSaveData> weaponSaveDatas;
    public List<VehicleSaveData> vehicleSaveDatas;
    public bool firstStart = false;
    public int selectedVehicleId; // Last vehicle used id
}
public class Progress : MonoBehaviour
{
    public PlayerInfo playerInfo;


    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            //Save();
            SetPlayerInfo();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        // Write JSON string to file
        string filePath = Path.Combine(Application.persistentDataPath, "savedata.json");

        // Check if file exists
        if (!File.Exists(filePath))
        {
            // Create the file if it doesn't exist
            File.Create(filePath).Close();
            Debug.Log("JSON file created: " + filePath);
        }

        // Convert data to JSON string
        string jsonString = JsonUtility.ToJson(playerInfo);

        // Write JSON string to file
        File.WriteAllText(filePath, jsonString);
        Debug.Log("JSON data written to file: " + filePath);
        Debug.Log("Saving...");
    }

    public void SetPlayerInfo()
    {
        // File path
        string filePath = Path.Combine(Application.persistentDataPath, "savedata.json");

        // Check if file exists
        if (File.Exists(filePath))
        {
            // Read JSON string from file
            string jsonString = File.ReadAllText(filePath);

            // Deserialize JSON string into PlayerData object
            playerInfo = JsonUtility.FromJson<PlayerInfo>(jsonString);
        }
        else
        {
            Save();
        }

        Debug.Log("Loading...");
    }
}
