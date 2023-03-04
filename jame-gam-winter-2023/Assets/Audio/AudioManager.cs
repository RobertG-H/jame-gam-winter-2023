using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Listens to events from audio channel scriptable objecst and plays audio
/// Attach to a gameobject in the scene to have audio be played
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject sfxPrefab;
    [SerializeField] AudioEventChannelSO audioEventChannel;
    bool disableAudio;

    void OnEnable()
    {
        audioEventChannel.OnAudioClipRequested += PlayAudio;
    }

    void OnDisable()
    {
        audioEventChannel.OnAudioClipRequested -= PlayAudio;
    }

    void OnRollbackStart()
    {
        //Disable audio during rollabck
        disableAudio = true;
    }

    void OnRollbackEnd()
    {
        disableAudio = false;
    }

    //TODO add transform parent support like FXManager
    public void PlayAudio(AudioClipSO audioClipSO, Vector3 position = default, Transform parent = null)
    {
        if(disableAudio)
            return;
        GameObject newSFX;
        if (parent != null)
        {
            newSFX = Instantiate(sfxPrefab, position, Quaternion.identity, parent);
        }
        else
        {
            newSFX = Instantiate(sfxPrefab, position, Quaternion.identity);
        }
        newSFX.GetComponent<DestroyAfterWait>().timeToDestroy = audioClipSO.audioClip.length;
        newSFX.name = $"Audio - {audioClipSO.audioClip.name}";

        
        AudioSource sfx = newSFX.GetComponent<AudioSource>();
        sfx.clip = audioClipSO.audioClip;
        sfx.volume = audioClipSO.volume + Random.Range(-audioClipSO.volume * audioClipSO.volumeVariationRatio, audioClipSO.volume * audioClipSO.volumeVariationRatio);
        sfx.pitch = audioClipSO.pitch + Random.Range(-audioClipSO.pitchVariationAmount, audioClipSO.pitchVariationAmount); 
        sfx.Play();
    }
}
