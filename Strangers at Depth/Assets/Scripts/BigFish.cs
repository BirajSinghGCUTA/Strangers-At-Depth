using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFish : MonoBehaviour
{
    public float speed = 10.0f; //speed of the fish moving from right to left
    private Rigidbody2D rb;     // rigid body of the fish
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>(); //get refrence to the required rigid body(In this cas BigFish)
        rb.velocity = new Vector2(speed, 0);//Move from right to left at a constant speed
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
  
    }
}
