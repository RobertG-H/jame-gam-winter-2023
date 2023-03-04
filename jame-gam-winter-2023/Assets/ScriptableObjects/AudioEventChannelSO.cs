// Script modified from Unity Technologies open project 1
// https://github.com/UnityTechnologies/open-project-1/blob/devlogs/2-scriptable-objects/UOP1_Project/Assets/Scripts/Events/ScriptableObjects/AudioCueEventChannelSO.cs

using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/Audio Event Channel")]
public class AudioEventChannelSO : ScriptableObject
{
	public UnityAction<AudioClipSO, Vector3, Transform> OnAudioClipRequested;

	public void RaiseEvent(AudioClipSO audioClip, Vector3 positionInSpace, Transform parent = null)
	{
		if (OnAudioClipRequested != null)
		{
			OnAudioClipRequested.Invoke(audioClip, positionInSpace, parent);
		}
		else
		{
			Debug.LogWarning("An AudioCue was requested, but nobody picked it up. " +
				"Check why there is no AudioManager already loaded, " +
				"and make sure it's listening on this AudioCue Event channel.");
		}
	}
}
