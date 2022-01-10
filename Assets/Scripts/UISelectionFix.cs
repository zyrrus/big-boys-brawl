using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISelectionFix : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
        startButton.Select();
    }

    void Update()
    {
        EventSystem e = EventSystem.current;
        Debug.LogFormat("{0} - {1}", gameObject.transform.parent.gameObject.transform.gameObject.transform.parent.name, e.currentSelectedGameObject);
    }
}
