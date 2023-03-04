using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartUI : MonoBehaviour
{
    VisualElement root;
    private void OnEnable ()
    {
        root = GetComponent<UIDocument> ().rootVisualElement;
        Button buttonStart = root.Q<Button> ("ButtonStart");

        buttonStart.clicked += OnStartClick;
    }

    void OnStartClick()
    {
        //hide UI
        root.visible = false;

        // TODO enable/disable controls

        GameManager.Instance.StartGame ();
    }
}
