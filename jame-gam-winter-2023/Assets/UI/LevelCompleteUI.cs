using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelCompleteUI : MonoBehaviour
{
    VisualElement root;
    private void OnEnable ()
    {
        root = GetComponent<UIDocument> ().rootVisualElement;
        Button buttonGoNext = root.Q<Button> ("ButtonGoNext");
            
        buttonGoNext.clicked += OnStartClick;
    }

    void OnStartClick ()
    {
        //hide UI
        root.visible = false;

        // TODO enable/disable controls

        GameManager.Instance.GoToNextScene ();
    }
}
