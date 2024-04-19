using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPause()
    {
        bool isLoaded = SceneManager.GetSceneByName("PauseMenu").isLoaded;
        if (!isLoaded)
        {
            GameObject.Find("Player").GetComponent<FPSController>().enabled = false;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }
            
        else
        {
            SceneManager.UnloadSceneAsync("PauseMenu");
            GameObject.Find("Player").GetComponent<FPSController>().enabled = true;
        }
            
    }
}
