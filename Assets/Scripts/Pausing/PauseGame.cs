using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseGame : MonoBehaviour
{

    public TextMeshProUGUI PauseTextUI;
    public GameObject overlay;
    public TextMeshProUGUI LevelUpScreen;



    // Start is called before the first frame update
    void Start()
    {
        PauseTextUI.enabled = false;
        overlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused();
        }

    }

    void Paused()
    {
        //when paused...
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            PauseTextUI.enabled = true;
            overlay.SetActive(true);
        }

        //when resumed
        else
        {
            if (LevelUpScreen.enabled == false)
            {
                Time.timeScale = 1;
                PauseTextUI.enabled = false;
                overlay.SetActive(false);
            }
        }
    }


}

