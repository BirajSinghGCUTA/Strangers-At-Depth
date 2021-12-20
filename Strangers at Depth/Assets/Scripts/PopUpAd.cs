using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PopUpAd : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public string popUp;
    public GameObject closeButton;

    public void PopUp(string text)
    {

        animator = GetComponent<Animator>();

        popUpBox.SetActive(true);
        popUpText.text = text;
        //animator.SetTrigger("pop");
         
        
        //PopUpAd pop = GetComponent<PopUpAd>();
        //pop.PopUp(popUp);
        
    }
    /*
    public int time = 0;

    //Use fixed update beacuase its called every fixed framerate frame
    
    void FixedUpdate()
    {

        if (!Input.anyKey)
        {

            //Starts counting when no button is being pressed
            time = time + 1;
        }
        else
        {

            // If a button is being pressed restart counter to Zero
            time = 0;
        }

        //Now after n frames of nothing being pressed it will do activate this if statement
        //50 frames per second
        //750 = 15 secs
        if (time == 50)
        {
            animator.SetTrigger("pop");
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Advertisement"));
            Debug.Log("500 frames passed with no input");

            //Now you could set time too zero so this happens every 100 frames
            //time = 0;
        }

    }
    */


    public void OnTerminateClick( )
    {
        Debug.Log("set trigger close");
        animator.SetTrigger("close");
        StartCoroutine(backtoScene());
        //SceneManager.UnloadSceneAsync("Advertisement");
    }

    IEnumerator backtoScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.UnloadSceneAsync("Advertisement");
        
    }

}
