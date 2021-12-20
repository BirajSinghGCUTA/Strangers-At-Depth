using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text winsText;
    public TMP_Text krakensText;

    public void NewScoreElement (string _username, int _wins, int _krakens)
    {
        usernameText.text = _username;
        winsText.text = _wins.ToString();
        krakensText.text = _krakens.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
