using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelSelection : MonoBehaviour
{
    public Color ReadyColor; 
    public Color NotReadyColor; 
    
    [SerializeField] private GameObject image; // Temporarily on Text object
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject readyObj; // Temporarily set on sprite
    [SerializeField] private Image readyImage;
    

    private void Start()
    {
        textMesh = image.GetComponent<TextMeshProUGUI>();
        readyImage = readyObj.GetComponent<Image>();
    }

    public void SetImage(string value) 
    {
        textMesh.text = value;
    }

    public void SetReady(bool state) 
    {
        if (state) readyImage.color = ReadyColor;
        else readyImage.color = NotReadyColor;
    }
}
