using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelSelection : MonoBehaviour
{
    public Color ReadyColor; 
    public Color NotReadyColor; 
    
    private static GameObject[] gmCharacters;

    [SerializeField] private GameObject imageParent;
    private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject readyParent;
    private Image readyImage;
    

    private void Start()
    {
        gmCharacters = GameManager.Instance.allCharacters;
        textMesh = imageParent.GetComponent<TextMeshProUGUI>();
        readyImage = readyParent.GetComponent<Image>();
        SetImage(0);
    }

    public void SetImage(int index) 
    {
        // Don't keep instantiating these
        // GameObject newImage = Instantiate(gmCharacters[index], imageParent.transform.position, imageParent.transform.rotation);
        // newImage.transform.SetParent(imageParent.transform);

        // Temporarily showing character names
        textMesh.text = gmCharacters[index].name;
    }

    public void SetReady(bool state) 
    {
        if (state) readyImage.color = ReadyColor;
        else readyImage.color = NotReadyColor;
    }
}
