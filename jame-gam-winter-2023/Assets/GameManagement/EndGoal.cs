using UnityEngine;

public class EndGoal : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO levelComplete;
    [SerializeField] VoidEventChannelSO interactEventChannel;
    [SerializeField] VoidEventChannelSO activatableEventChannel;

    ShellManager shellManager;
    bool activatable = false;

    void OnEnable()
    {
        interactEventChannel.OnEvent += OnInteract;
    }

    void OnDisable ()
    {
        interactEventChannel.OnEvent -= OnInteract;
    }

    void OnInteract()
    {
        if (activatable)
        {
            levelComplete.RaiseEvent ();
            if (shellManager != null)
            {
                shellManager.AttachShell (this.gameObject);
                Debug.Log ("attaching shell");

            }
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            if (shellManager == null)
            {
                shellManager = other.GetComponent<ShellManager> ();
            }
            activatable = true;
            activatableEventChannel.RaiseEvent ();
            Debug.Log ("activatable");
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            activatable = false;
            activatableEventChannel.RaiseEvent ();
            Debug.Log ("NOT activatable");
        }
    }
}
