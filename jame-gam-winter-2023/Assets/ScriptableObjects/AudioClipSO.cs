// Script modified from Unity Technologies open project 1
// https://github.com/UnityTechnologies/open-project-1/blob/devlogs/2-scriptable-objects/UOP1_Project/Assets/Scripts/Audio/AudioData/AudioCueSO.cs

using UnityEngine;
[CreateAssetMenu(fileName = "newAudioClip", menuName = "Audio/Audio Clip")]
public class AudioClipSO : ScriptableObject
{
    public AudioClip audioClip = default;

    [Range(0, 1)]
    public float volume = 1;
    public float pitch = 1;

    [Range(0, 1)]
    public float volumeVariationRatio = 0.05f;

    [Range(0, 1)]
    public float pitchVariationAmount = 0.05f;
}

