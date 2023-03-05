using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour, ITriggerOnMultiply
{
    [SerializeField] Transform propulsionLocation;
    public void MultiplyEvent()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForceAtPosition(transform.forward * 1000, propulsionLocation.position);
        Debug.Log("Running");
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.Play();
    }
}
