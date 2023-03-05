using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    [SerializeField] CharacterStateMachine stateMachine;
    bool playingAudio = false;
    bool playingMove = false;
    float crossFadeAmount = 0.3f;
    [SerializeField] AudioEventChannelSO audioEventChannelSO;
    [SerializeField] List<AudioClipSO> audioClips;

    IEnumerator moveAudio;
    private void Update ()
    {
        PlayMoveAudio();
    }

    void PlayMoveAudio()
    {
        if(playingMove)
            return;
        if (stateMachine.Movement.IsGrounded() && stateMachine.MoveDirection != Vector3.zero)
        {
            moveAudio = PlayMoveAudioAndWait();
            StartCoroutine (moveAudio);
        }
        else
        {
            if(moveAudio != null)
            {
                StopCoroutine(moveAudio);
            }
        }
    }


    IEnumerator PlayAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds (delay);
        AudioClipSO randomClip = audioClips [Random.Range (0, audioClips.Count)];
        audioEventChannelSO.RaiseEvent (randomClip, transform.parent.position, transform.parent);
        playingAudio = false;
    }

    IEnumerator PlayMoveAudioAndWait()
    {
        playingMove = true;
        AudioClipSO movementClip = audioClips [2];
        audioEventChannelSO.RaiseEvent (movementClip, transform.parent.position, transform.parent);
        yield return new WaitForSeconds (movementClip.audioClip.length - crossFadeAmount);
        playingMove = false; 
   }
}
