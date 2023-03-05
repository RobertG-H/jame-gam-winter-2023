using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    [SerializeField] CharacterStateMachine stateMachine;
    bool playingAudio = false;
    [SerializeField] AudioEventChannelSO audioEventChannelSO;
    [SerializeField] List<AudioClipSO> audioClips;
    private void Update ()
    {
        if (playingAudio)
            return;
        if (stateMachine.Movement.IsGrounded() && stateMachine.MoveDirection != Vector3.zero)
        {
            playingAudio = true;
            StartCoroutine ("playAudioAfterDelay", Random.Range (1.5f, 4.5f));
        }
        else
        {
            StopAllCoroutines ();
            playingAudio = false;
        }
    }

    IEnumerator playAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds (delay);
        AudioClipSO randomClip = audioClips [Random.Range (0, audioClips.Count)];
        audioEventChannelSO.RaiseEvent (randomClip, transform.parent.position, transform.parent);
        playingAudio = false;
    }
}
