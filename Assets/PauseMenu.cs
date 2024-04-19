using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
