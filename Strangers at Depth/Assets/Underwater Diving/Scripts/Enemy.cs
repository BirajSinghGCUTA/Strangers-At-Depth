using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	//private PlayerController thePlayer;
	public GameObject death;

	public float speed = 0.3f;

	private float turnTimer;
	public float timeTrigger;
	private Vector2 screenBounds;
	private Rigidbody2D myRigidbody;


	// Use this for initialization
	void Start () {
		//thePlayer = FindObjectOfType<PlayerController> ();	
		myRigidbody = GetComponent<Rigidbody2D> ();
		screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		turnTimer = 0;
		timeTrigger = 3f;	 
	}

	// Update is called once per frame
	void Update (){
		myRigidbody.velocity = new Vector3 (myRigidbody.transform.localScale.x * speed, myRigidbody.velocity.y, 0f);

		turnTimer += Time.deltaTime;
		if(turnTimer >= timeTrigger && (transform.position.x > screenBounds.x || transform.position.x < (-screenBounds.x))){
			turnAround ();
			turnTimer = 0;
		}
	}

	//void OnTriggerEnter2D(Collider2D other){

		//if(other.tag == "Player" && thePlayer.rushing){
			//Instantiate (death, gameObject.transform.position, gameObject.transform.rotation);
			//Destroy (gameObject);
		//}

	//}

	void turnAround(){
		if (transform.localScale.x == 1) {
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		} else {
			transform.localScale = new Vector3 (1f,1f,1f);
		}
	}
}
