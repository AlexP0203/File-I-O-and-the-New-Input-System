using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using UnityEngine.Events;

public class SaveHandler : MonoBehaviour
{
    FPSController player;
    Gun gun;
    PlayerHUD playerStats;
    string path;

    public void Save()
    {
        
        SaveData sd = new SaveData();
        sd.playerPosition = FindObjectOfType<GunAim>().transform.position;
        sd.ammoAmount = gun.GetAmmo();
        sd.playerHealth = playerStats.currentHealth();
        string jsonText = JsonUtility.ToJson(sd);
        File.WriteAllText(path, jsonText);
    }

    public void Load()
    {
        try
        {
            player.DisablePlayerController();
            string saveText = File.ReadAllText(path);
            SaveData myData = JsonUtility.FromJson<SaveData>(saveText);
            gun.AddAmmo(myData.ammoAmount);
            playerStats.updateCurrentAmmo(myData.ammoAmount);
            playerStats.updateHealthBarFromSave(myData.playerHealth);
            FindObjectOfType<GunAim>().transform.position = myData.playerPosition;
            player.DisablePlayerController();
            SceneManager.UnloadSceneAsync("PauseMenu");
            GameObject.Find("Player").GetComponent<FPSController>().enabled = true;
        }

        catch (System.IO.FileNotFoundException e)
        {
            Debug.Log("NO SAVE FILE FOUND");
            player.DisablePlayerController();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FPSController>();
        gun = FindObjectOfType<Gun>();
        playerStats = FindObjectOfType<PlayerHUD>();
        path = Application.persistentDataPath + "/SaveData.json";
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SaveData sd = new SaveData();
            sd.playerPosition = FindObjectOfType<GunAim>().transform.position;
            sd.ammoAmount = gun.GetAmmo();
            sd.playerHealth = playerStats.currentHealth();
            string jsonText = JsonUtility.ToJson(sd);
            File.WriteAllText(path, jsonText);
        }

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            try
            {
                player.DisablePlayerController();
                string saveText = File.ReadAllText(path);
                SaveData myData = JsonUtility.FromJson<SaveData>(saveText);
                gun.AddAmmo(myData.ammoAmount);
                playerStats.updateCurrentAmmo(myData.ammoAmount);
                playerStats.updateHealthBarFromSave(myData.playerHealth);
                FindObjectOfType<GunAim>().transform.position = myData.playerPosition;
                player.DisablePlayerController();
            }
            
            catch(System.IO.FileNotFoundException e)
            {
                Debug.Log("NO SAVE FILE FOUND");
                player.DisablePlayerController();
            }
        }
    }
}

public class SaveData
{
    public Vector3 playerPosition;
    public int ammoAmount;
    public float playerHealth;
}




