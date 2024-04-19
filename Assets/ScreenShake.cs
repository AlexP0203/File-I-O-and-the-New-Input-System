using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera vcam;
    float endShake;
    // Start is called before the first frame update
    void Start()
    {
        var gun = FindAnyObjectByType<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        endShake += 0.01f;
        if (endShake > .15f)
        {
            EndShake();
        }

    }
    [ContextMenu("Start Shake")]
    public void StartShake()
    {
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 25;

        endShake = 0f;
    }
    [ContextMenu("End Shake")]


    public void EndShake()
    {
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;

    }
}
