using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NonPlayerCharacters : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    float timerDisplay;
    private void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }
    void Update()
    {
        if (timerDisplay >=0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay<0)
            {
                dialogBox.SetActive(false);
            }
        }
    }
    public void DisplayDialog()
    { 
        timerDisplay= displayTime;
        dialogBox.SetActive(true);
    }
}
