using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathText : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO deathEvent;
    VisualElement root;

    void OnEnable ()
    {
        deathEvent.OnEvent += OnDeath;
        root = GetComponent<UIDocument> ().rootVisualElement;
        root.visible = false;
    }

    void OnDisable ()
    {
        deathEvent.OnEvent -= OnDeath;
    }

    void OnDeath()
    {
        root.visible = true;
    }
}
