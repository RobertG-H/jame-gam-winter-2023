using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;


public class ActivatableUI : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO activatableEventChannel;
    [SerializeField] VoidEventChannelSO levelComplete;

    VisualElement root;
    bool isActive = false;
    bool disabled = false;

    private void Awake ()
    {
        root = GetComponent<UIDocument> ().rootVisualElement;
        root.visible = false;

    }

    private void OnEnable ()
    {
        activatableEventChannel.OnEvent += OnActivate;
        levelComplete.OnEvent += OnLevelComplete;
    }

    private void OnDisable ()
    {
        activatableEventChannel.OnEvent -= OnActivate;
        levelComplete.OnEvent -= OnLevelComplete;

    }

    void OnActivate()
    {
        if (disabled)
            return;
        isActive = !isActive;
        root.visible = isActive;
    }

    void OnLevelComplete()
    {
        disabled = true;
        root.visible = false;
    }
}
