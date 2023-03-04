using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channel/Void")]
public class VoidEventChannelSO : ScriptableObject
{
    public UnityAction OnEvent;

    public void RaiseEvent()
    {
        if (OnEvent != null)
            OnEvent.Invoke();
    }
}
