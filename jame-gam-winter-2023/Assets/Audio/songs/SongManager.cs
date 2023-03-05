using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField] float audioStartOffset = 0.5f;
    [SerializeField] VoidEventChannelSO gameStartEventChannel;
    
    [SerializeField] AudioSource noLoopAudioSource;
    [SerializeField] AudioSource loopAudioSource;

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
        loopAudioSource.Stop ();
        loopAudioSource.clip = tubaSong;
        loopAudioSource.Play ();
    }

    private void Awake ()
    {
        noLoopAudioSource.clip = titleIntro;
        loopAudioSource.clip = titleSong;
    }

    private void Start ()
    {
        noLoopAudioSource.PlayDelayed (audioStartOffset);
        loopAudioSource.PlayDelayed (2.116f + audioStartOffset);
    }
}
