using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject selector;
    [SerializeField] private GameObject character;

    private void Start()
    {
        
    }

    public void SelectCharacter(GameObject a) {
        character = a;
    }


}
