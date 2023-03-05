using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartUI : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO gameStartEventChannel;
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
        Debug.Log ("STARTING GAME");
        gameStartEventChannel.RaiseEvent ();

        GameManager.Instance.StartGame ();
    }
}
