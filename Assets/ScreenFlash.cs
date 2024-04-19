using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    [SerializeField] Color targetColor;
    [SerializeField] Image flashImage;
    [SerializeField] float flashSpeed;
    FPSController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FPSController>();
    }

    // Update is called once per frame
    void Update()
    {

        var currentColor = flashImage.color;

        currentColor = Color.Lerp(currentColor, targetColor, 2 * Time.deltaTime);

        flashImage.color = currentColor;
    }

    public void damageFlash()
    {
        flashImage.color = Color.red;
    }
}
