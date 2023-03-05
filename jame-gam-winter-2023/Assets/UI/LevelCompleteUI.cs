using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO levelComplete;
    VisualElement root;

    void OnEnable ()
    {
        levelComplete.OnEvent += OnLevelComplete;
        root = GetComponent<UIDocument> ().rootVisualElement;
        //Button buttonGoNext = root.Q<Button> ("ButtonGoNext");

        //buttonGoNext.clicked += OnStartClick;
        root.visible = false;
    }

    void OnDisable ()
    {
        levelComplete.OnEvent -= OnLevelComplete;
    }

    void OnLevelComplete()
    {
        Debug.Log ("LEVEL COMPLETE");
        root.visible = true;
    }

    //void OnStartClick ()
    //{
    //    //hide UI
    //    root.visible = false;

    //    // TODO enable/disable controls

    //    GameManager.Instance.GoToNextScene ();
    //}
}
