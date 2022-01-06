using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance 
    {
        get;
        set;
    }

    private void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
    }

    private void Start() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
