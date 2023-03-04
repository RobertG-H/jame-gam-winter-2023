using UnityEngine;

public class EndGoal : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO levelComplete;

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            levelComplete.RaiseEvent();
        }
    }
}
