using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField] float audioStartOffset = 0.5f;
    [SerializeField] VoidEventChannelSO gameStartEventChannel;
    
    [SerializeField] AudioSource noLoopAudioSource;
    [SerializeField] AudioSource titleLoopAudioSource;
    [SerializeField] AudioSource tubaAudioSource;

    [SerializeField] AudioClip titleIntro;
    [SerializeField] AudioClip titleSong;
    [SerializeField] AudioClip tubaSong;

    private void OnEnable ()
    {
        gameStartEventChannel.OnEvent += OnGameStart;
    }

    private void OnDisable ()
    {
        gameStartEventChannel.OnEvent -= OnGameStart;
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
    }

    private void Start ()
    {
        noLoopAudioSource.PlayDelayed (audioStartOffset);
        titleLoopAudioSource.PlayDelayed (2.116f + audioStartOffset);
    }
}
