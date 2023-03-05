using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticlesAfterComplete : MonoBehaviour
{
    // Destroys the game object when the particle system stops playing
    ParticleSystem ps;    
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(!ps.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
