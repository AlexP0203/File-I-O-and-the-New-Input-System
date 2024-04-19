using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleActivator : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        var gun = FindAnyObjectByType<Gun>();
    }

    public void Emit()
    {
        ps.Play();
    }
}
