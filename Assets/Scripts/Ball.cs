using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    

    public void Launch(float forceToMove) 
    {
        this.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * forceToMove, ForceMode.Impulse);
    }

    public float CalculateHoldDownForce(float holdSwipe) 
    {
        float maxHoldDownSwipe = 800f;
        float holdSwipeNormalized = Mathf.Clamp01(holdSwipe/maxHoldDownSwipe);
        float force = holdSwipeNormalized * 100f;
        //Debug.Log(holdSwipeNormalized);
        return force;
    }
}//class1
