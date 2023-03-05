using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;


public class ActivatableUI : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO activatableEventChannel;
    VisualElement root;
    bool isActive = false;

    private void Awake ()
    {
        root = GetComponent<UIDocument> ().rootVisualElement;
        root.visible = false;

    }

    private void OnEnable ()
    {
        activatableEventChannel.OnEvent += OnActivate;
    }

    private void OnDisable ()
    {
        activatableEventChannel.OnEvent -= OnActivate;
    }

    void OnActivate()
    {
        isActive = !isActive;
        root.visible = isActive;
    }
}
