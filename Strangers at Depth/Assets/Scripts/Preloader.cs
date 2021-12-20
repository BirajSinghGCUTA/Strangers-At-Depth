using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{

    private CanvasGroup fadeGroup;
    private float loadTime;
    private readonly float minLogoTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();

        fadeGroup.alpha = 1;

        //Pre load the game
        if (Time.time < minLogoTime)
        {
            loadTime = minLogoTime;
        }   
        else
        {
            loadTime = Time.time;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < minLogoTime)
        {
            fadeGroup.alpha = 1 - Time.time;
        }

        if(Time.time > minLogoTime && loadTime != 0)
        {
            fadeGroup.alpha = Time.time - minLogoTime;
            if(fadeGroup.alpha >= 1)
            {
                //SceneManager.LoadScene("HomeScreenScene");
            }
        }
    }
}
