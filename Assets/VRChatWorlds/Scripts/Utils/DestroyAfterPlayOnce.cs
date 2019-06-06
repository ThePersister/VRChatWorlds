using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyAfterPlayOnce : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, GetComponent<ParticleSystem>().main.startLifetime.constant);
    }
}
