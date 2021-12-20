using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTransparency : MonoBehaviour
{
    RaycastHit2D hit;
    public Transform targetPlayer;
    public Transform enemyPlayer;
    float distance;
    public float startalpha = 0.3f;
    CanvasGroup buttonTransparency;
    Vector2 EnemyPosition;
    Vector2 playerPosition;
    public LayerMask except;
    private void Start()
    {
        buttonTransparency = GetComponent<CanvasGroup>();
        buttonTransparency.alpha = startalpha;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        EnemyPosition = new Vector2(enemyPlayer.transform.position.x, enemyPlayer.transform.position.y);
        playerPosition = new Vector2(targetPlayer.transform.position.x, targetPlayer.transform.position.y);
        hit = Physics2D.Raycast(playerPosition, EnemyPosition - playerPosition, except);
        if (hit.collider != null)
        {
            distance = hit.point.sqrMagnitude;
            Debug.Log("Player Distance is " + distance.ToString());
            Debug.DrawLine(playerPosition, (EnemyPosition- playerPosition) * 100, Color.red);
            /*if (distance < 5.0f)
            {
                for (float i = startalpha; i <= 1.0f; i += 0.1f)
                {
                    //buttonTransparency.alpha = i;
                    Debug.Log("I equals " + i.ToString());
                }
                //buttonTransparency.alpha = 1.0f;
            }*/
        }
           
        
    }
}
