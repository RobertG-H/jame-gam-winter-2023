using UnityEngine;

public class EndGoal : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.LevelComplete ();
        }
    }
}
