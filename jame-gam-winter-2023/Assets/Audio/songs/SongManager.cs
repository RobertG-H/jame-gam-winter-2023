using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField] float audioStartOffset = 0.5f;
    [SerializeField] VoidEventChannelSO gameStartEventChannel;
    [SerializeField] VoidEventChannelSO onFireEventChannel;


    [SerializeField] AudioSource noLoopAudioSource;
    [SerializeField] AudioSource titleLoopAudioSource;
    [SerializeField] AudioSource tubaAudioSource;
    [SerializeField] AudioSource breakbeatAudioSource;
    [SerializeField] AudioSource breakbeatAudioSource2;



    [SerializeField] AudioClip titleIntro;
    [SerializeField] AudioClip titleSong;
    [SerializeField] AudioClip tubaSong;

    private void OnEnable ()
    {
        gameStartEventChannel.OnEvent += OnGameStart;
        onFireEventChannel.OnEvent += OnFire;
    }

    private void OnDisable ()
    {
        gameStartEventChannel.OnEvent -= OnGameStart;
        onFireEventChannel.OnEvent -= OnFire;

    }

    void OnGameStart ()
    {
        titleLoopAudioSource.Stop ();
        tubaAudioSource.clip = tubaSong;
        tubaAudioSource.Play ();
    }

    private void Awake ()
    {
        noLoopAudioSource.clip = titleIntro;
        titleLoopAudioSource.clip = titleSong;
        breakbeatAudioSource.volume = 0;
        breakbeatAudioSource2.volume = 0;

    }

    private void Start ()
    {
        noLoopAudioSource.PlayDelayed (audioStartOffset);
        titleLoopAudioSource.PlayDelayed (2.116f + audioStartOffset);
        breakbeatAudioSource.PlayDelayed (2.116f + audioStartOffset);
        breakbeatAudioSource2.PlayDelayed (2.116f + audioStartOffset);


    }

    void OnFire()
    {
        breakbeatAudioSource.volume += 0.0015f;
        breakbeatAudioSource2.volume += 0.0009f;

        if (breakbeatAudioSource.volume > 1)
        {
            breakbeatAudioSource.volume = 1;
        }
        if (breakbeatAudioSource2.volume > 1)
        {
            breakbeatAudioSource2.volume = 1;
        }
    }
}
