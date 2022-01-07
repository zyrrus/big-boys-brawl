using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public static void ClickPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void ClickQuit()
    {
        Application.Quit();
    }

    public static void SelectFirstChildOf(GameObject parent) {
        GameObject firstChild = parent.transform.GetChild(0).gameObject;
        EventSystem es = EventSystem.current;

        es.SetSelectedGameObject(firstChild);
    }

    public static void SetFullscreen() {}
    public static void SetResolution() {}
    public static void SetVolume() {}
}
