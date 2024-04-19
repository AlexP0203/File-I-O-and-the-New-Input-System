using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image healthBar;
    TMP_Text currentAmmoText;

    FPSController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FPSController>();
        var gun = FindAnyObjectByType<Gun>();
        var reload = FindAnyObjectByType<Reload>();
        currentAmmoText = GameObject.Find("Ammo Current Text").GetComponent<TMP_Text>();
        //var controller = FindAnyObjectByType<FPSController>();
    }

    public void updateCurrentAmmo(int currentA)
    {

        currentAmmoText.text = currentA.ToString();
    }

    public void updateHealthBar()
    {
        healthBar.fillAmount -= 0.1f;
    }

    public void updateHealthBarFromSave(float h)
    {
        healthBar.fillAmount = h;
    }

    public float currentHealth()
    {
        return healthBar.fillAmount;
    }

}
