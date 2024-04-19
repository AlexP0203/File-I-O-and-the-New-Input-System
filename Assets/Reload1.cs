using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Reload : MonoBehaviour
{

    [SerializeField] UnityEvent<int> reload;
    FPSController player;
    bool triggerActive = false;
   
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FPSController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (triggerActive)
        {
            if (Input.GetButtonDown("Interact"))
            {
                reload.Invoke(12);
            }
                
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        triggerActive = true;
    }

    public void OnTriggerExit(Collider other)
    {
        triggerActive = false;
    }
}
