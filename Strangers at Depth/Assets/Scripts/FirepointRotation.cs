using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirepointRotation : MonoBehaviour
{
    public int rotationOffset = 0;

    // Update is called once per frame
    void Update()
    {
        
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - transform.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + rotationOffset);
    }
}
