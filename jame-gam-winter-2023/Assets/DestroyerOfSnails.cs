using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerOfSnails : MonoBehaviour, ITriggerOnMultiply
{
    [SerializeField] GameObject Pistol;
    [SerializeField] AudioClipSO gunShot;
    public void MultiplyEvent()
    {
        Pistol.SetActive(true);
    }
}
