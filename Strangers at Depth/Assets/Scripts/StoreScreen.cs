using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class StoreScreen : MonoBehaviour
{
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    public Text krakensHeld;
    public GameObject noMoreItems;
    public GameObject popUp;
    public GameObject popUp2;
    public GameObject popUp3;
    public bool isPopup = false;
    private CanvasGroup fadeGroup;
    private readonly float fadeInSpeed = .33f;
    public Transform ItemCategories;
    public Transform ItemDisplay;
    public Button[] skins;
    public GameObject ShowMore;
    public GameObject ShowPrevious;
    public int currentIndex;
    public string skinOwned;
    public bool NextPage;
    // Start is called before the first frame update
    void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();

        fadeGroup.alpha = 1;

        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        checkToDestroy();
        
        StartCoroutine(destroyB());
        
        
        //InitShop();
        
        //attempt();

    }

    void Awake()
    {
        
        displayKrakens();
        
    }

    // Update is called once per frame
    void Update()
    {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
        //InitShop();
    }

    private void InitShop()
    {
        

        if (ItemCategories == null || ItemDisplay == null)
        {
            Debug.Log("Error");
        }


        int i = 0;
        foreach (Transform t in ItemCategories)
        {
            
            int currentIndex = i;

            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnItemCategoriesSelect(currentIndex));
            i++;
        }

        //checkToDestroy();
        



        i = 0;
        foreach (Transform t in ItemDisplay)
        {
            /*FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Skin owned?").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle the error...
                    Debug.Log("Failed to load Krakens held");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    //string ss = snapshot.Child("Total Krakens").Value.ToString();
                    Debug.Log("" + t.gameObject.name);
                    if (snapshot.Value.ToString() == "1")
                    {
                        t.gameObject.SetActive(false);
                        //destroyButton("Item1");
                    }
                    //krakensHeld.gameObject.SetActive(true);
                    // Do something with snapshot...
                }
                else
                {
                    Debug.Log("Not Working");
                }
            });*/
            //Debug.Log(""+ t.name);
            //Debug.Log(t.gameObject.name);
            //Debug.Log("" + t.gameObject.name.ToString());
            /*if(skinOwned == "1")
            {
                Debug.Log("Skin is owned");
                if (t.gameObject.name == "Item1")
                {
                    Debug.Log("Found Item");
                    t.gameObject.SetActive(false);
                }
            }*/

            Button b = t.GetComponent<Button>();
            if(b.isActiveAndEnabled)
            {
                i++;
            }
            //i++;
            
        }
        
        skins = new Button[i];

        
        i = 0;
        foreach (Transform t in ItemDisplay)
        {
            int currentIndex = i;
            
            Button b = t.GetComponent<Button>();
            if (b.isActiveAndEnabled)
            {
                skins[i] = b;
                b.onClick.AddListener(() => OnItemDisplaySelect(currentIndex));
                i++;
            }
            
        }

        if (skins.Length >= 9)
        {
            ShowMore.SetActive(true);
            for (i = 8; i < skins.Length; i++)
            {
                skins[i].gameObject.SetActive(false);
            }
        }
        else if (skins.Length == 0)
        {
            noMoreItems.SetActive(true);
            ItemDisplay.gameObject.SetActive(false);
        }
        else
        {
            ShowMore.SetActive(false);
            ShowPrevious.SetActive(false);
            /*for (i = 0; i < 8; i++)
            {
                skins[i].gameObject.SetActive(true);
            }*/
        }
    }

    private void OnItemCategoriesSelect(int currentIndex)
    {
        //Retrieve new thumbnail images in ItemDisplaySelect
        //If no more items in that category, pop up message
        if (currentIndex == 0)
        {

        }
        else
        {
            if (isPopup == false)
            {
                isPopup = true;
                popUp2.SetActive(true);

            }
        }
        Debug.Log("Category " + currentIndex);
        
    }

    private void OnItemDisplaySelect(int currentIndex)
    {
        Debug.Log("Item " + currentIndex);
        //Retrieve skin at current index and set as the Skin preview
        if (currentIndex == 0 && skinOwned == "0")
        {
            if (isPopup == false)
            {
                isPopup = true;
                popUp.SetActive(true);

            }
        }
        else
        {
            if (isPopup == false)
            {
                isPopup = true;
                popUp3.SetActive(true);

            }
        }
        
        

    }



    public void OnXButtonClick()
    {
        SceneManager.LoadScene("HomeScreenScene");
        Debug.Log("Go to HomeScreen");
    }

    public void OnShowMoreClick()
    {
        NextPage = true;
        for (int i = 0; i < 8; i++)
        {
            skins[i].gameObject.SetActive(false);
        }
        for (int i = 8; i < skins.Length; i++)
        {
            skins[i].gameObject.SetActive(true);
        }
        //InitShop();
        Debug.Log("Show More");
    }

    public void OnShowPreviousClick()
    {
        NextPage = false;
        for (int i = 0; i < 8; i++)
        {
            skins[i].gameObject.SetActive(true);
        }
        for (int i = 8; i < skins.Length; i++)
        {
            skins[i].gameObject.SetActive(false);
        }
        //InitShop();
        Debug.Log("Show Previous");
    }

    public void onClosePopUp()
    {
        isPopup = false;
        popUp.SetActive(false);
        popUp2.SetActive(false);
        popUp3.SetActive(false);
        //Start();
        //InitShop();
    }

    public void onPurchasePopUp()
    {
        int krakens = 0;
        int.TryParse(krakensHeld.text, out krakens);
        if (krakens >= 100)
        {
            krakens = krakens - 100;
            krakensHeld.text = krakens.ToString();
            addKraken(krakens);
            ownSkin();
            checkToDestroy();
            StartCoroutine(destroyB());
            updateText();
            onClosePopUp();
        }
        else
        {
            
        }

    }

    public void onPurchasePopUp3()
    {
        //addKraken2();
        destroyButton();
        //StartCoroutine(destroyB());
        updateText();
        onClosePopUp();
    }

    private void displayKrakens()
    {
        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Total Krakens").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("Failed to load Krakens held");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //string ss = snapshot.Child("Total Krakens").Value.ToString();
                //Debug.Log(ss);
                krakensHeld.text = snapshot.Value.ToString();
                //krakensHeld.gameObject.SetActive(true);
                // Do something with snapshot...
            }
            else
            {
                Debug.Log("Not Working");
            }
        });

    }

    private void addKraken(int krakens)
    {
        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Total Krakens").SetValueAsync(krakens);
        displayKrakens();
        
    }

    private void ownSkin()
    {
        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Skins Owned").SetValueAsync(1);
        //attempt();

    }

    private void addKraken2()
    {
        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Skins Owned").SetValueAsync(0);
        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Total Krakens").SetValueAsync(500);
        //attempt();

    }

    private void updateText()
    {
        krakensHeld.gameObject.SetActive(false);
        krakensHeld.gameObject.SetActive(true);
    }

    private void destroyButton()
    {
        //GameObject noButton = new GameObject("noButton");
        int i = 0;
        if (skins[i].gameObject.name != "Item1")
        {
            skins[i].gameObject.SetActive(false);
            InitShop();
        }
        
    }

    private void checkToDestroy()
    {
        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Skins Owned").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("Failed to load Krakens held");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //string ss = snapshot.Child("Total Krakens").Value.ToString();
                skinOwned = snapshot.Value.ToString();
                
                // Do something with snapshot...
            }
            else
            {
                Debug.Log("Not Working");
            }
        });
        


    }

    IEnumerator destroyB()
    {
        yield return new WaitForSeconds(.1f);
        if (skinOwned == "1")
        {
            foreach (Transform t in ItemDisplay)
            {
                if (t.gameObject.name == "Item1")
                {
                    t.gameObject.SetActive(false);
                    //Destroy(t.gameObject);
                    
                }
            }
            for (int i = 8; i < skins.Length; i++)
            {
                skins[i].gameObject.SetActive(true);
            }
            InitShop();
        }
        else
        {
            InitShop();
        }
    }




}
