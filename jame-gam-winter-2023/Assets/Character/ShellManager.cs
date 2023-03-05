using UnityEngine;

public class ShellManager : MonoBehaviour
{
    [SerializeField] Transform shellAttachTarget;
    [SerializeField] AudioClipSO clip;
    [SerializeField] AudioEventChannelSO audioChannel;
    public void AttachShell(GameObject shell)
    {
        shell.transform.SetParent (shellAttachTarget);
        shell.transform.localPosition = Vector3.zero;
        shell.transform.localRotation = Quaternion.identity;
        shell.transform.localScale = new Vector3 (1,1,1);
        shell.GetComponent<Rigidbody> ().isKinematic = true;
        audioChannel.RaiseEvent (clip, transform.position);
    }
}
