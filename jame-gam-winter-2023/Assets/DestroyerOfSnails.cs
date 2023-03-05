using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerOfSnails : MonoBehaviour, ITriggerOnMultiply
{
    [SerializeField] GameObject Pistol;
    [SerializeField] AudioClipSO gunShotSound;
    [SerializeField] AudioEventChannelSO sfxEventChannel;
    [SerializeField] VoidEventChannelSO DeathEventChannel;
    public void MultiplyEvent()
    {
        Pistol.SetActive(true);
        Transform playerTransform = FindObjectOfType<CharacterMovement>().transform;
        transform.position =  playerTransform.forward * 3.5f + playerTransform.position;
        transform.forward = playerTransform.forward;
        sfxEventChannel.RaiseEvent(gunShotSound, Pistol.transform.position);
        StartCoroutine(DeathAfterDelay());
    }

    IEnumerator DeathAfterDelay()
    {
        yield return new WaitForSeconds(1.2f);
        DeathEventChannel.RaiseEvent();
    }
}
