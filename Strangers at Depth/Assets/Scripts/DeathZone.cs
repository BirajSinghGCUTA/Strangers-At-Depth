using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    public GameObject target;
    public GameObject platformPrefab;
    public GameObject coinPlatformPrefab;
    private GameObject platform;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (Random.Range(1,6) > 1)
        {
            platform = (GameObject)Instantiate(platformPrefab, new Vector2(Random.Range(-6f, 6f), target.transform.position.y + 4.7f), Quaternion.identity);
        }
        else
        {
            platform = (GameObject)Instantiate(coinPlatformPrefab, new Vector2(Random.Range(-6f, 6f), target.transform.position.y + 4.7f), Quaternion.identity);
        }
        
        Destroy(collision.gameObject);
        //BoxCollider2D platformBoxCollider = platform.GetComponent<BoxCollider2D>();
        //CircleCollider2D platformCircleCollider = platform.GetComponent<CircleCollider2D>();
        //platformBoxCollider.enabled = false; // Disable object's own collider to prevent detecting itself
        //platformCircleCollider.enabled = false; // Disable object's own collider to prevent detecting itself

        // while collider overlaps, move your object somewhere else (e.g. 17 units up)
        /*while (Physics2D.OverlapBox(platformBoxCollider.bounds.center, platformBoxCollider.size, 0) != null) //|| Physics2D.OverlapCircle(platformCircleCollider.bounds.center, platformCircleCollider.radius, 0) != null)
        {
            if (platform.transform.position.x <= 0)
            {
                platform.transform.Translate(new Vector3(2.5f, -2f));
            }
            else
            {
                platform.transform.Translate(new Vector3(-2.5f, 2f));
            }
        }*/

        //platformBoxCollider.enabled = true; // enable the collider again
        //platformCircleCollider.enabled = true;
        
    }
}
