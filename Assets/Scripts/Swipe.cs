using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Swipe : MonoBehaviour
{   
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 startTouch, swipeDelta;
    private bool isDragging;
    
    public float deadZone = 125f;
   
    public Ball ball;
    public GameObject extrude;
    public Vector3 extrudeOffset;
    private bool onTheMove = true;




    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    //public bool SwipeLeft { get { return swipeLeft; } }
    //public bool SwipeRight{ get { return swipeRight; } }
    //public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    

    // Update is called once per frame
    void Update()
    {
        //if (this.transform.childCount > 1)
        //{
        //    for (int i = 1; i < this.transform.childCount; i++) 
        //    {
        //        Destroy(this.transform.GetChild(i).gameObject);
        //    }
        //}

        if (this.transform.position.y > 255f) 
        {
            SceneManager.LoadScene(1);
        }

        Stick[] goSticks = FindObjectsOfType<Stick>();
        for(int i = 1; i < goSticks.Length; i++) 
        {
            Destroy(goSticks[i].gameObject);
        }


        #region MobileIputs
        if (Input.touches.Length > 0) 
        {
            if (Input.touches[0].phase == TouchPhase.Began) 
            {
                tap = true;
                
                isDragging = true;
                startTouch = Input.touches[0].position;
                
            }

            //if (Input.touches[0].phase == TouchPhase.Moved)
            //{
            //    Debug.Log("Moved");
            //    onTheMove = true;
            //    isDragging = true;
            //    startTouch = Input.touches[0].position;
            //}

            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) 
            {
               
                isDragging = false;
                if (swipeDown)
                {
                    this.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    ball.Launch(ball.CalculateHoldDownForce(Mathf.Abs(SwipeDelta.y)));
                   // DestroyExtrude();
                }
                Reset();
            }
        }
        #endregion

        //calculate the distance
        swipeDelta = Vector2.zero;
        if (isDragging) 
        {
            if (Input.touches.Length > 0) 
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }

            else if (Input.GetMouseButton(0)) 
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        //did we cross the deadzone
        if (Mathf.Abs(swipeDelta.y) > deadZone)
        {
            
            //float x = swipeDelta.x;
            float y = swipeDelta.y;
            //Debug.Log(y);
            if (y < 0)
               {
                   swipeDown = true;
               }
            

            //if (Mathf.Abs(x) > Mathf.Abs(y)) 
            //{
            //    //if (x < 0) 
            //    //{
            //    //    swipeLeft = true;
            //    //}

            //    //else 
            //    //{
            //    //    swipeRight = true;
            //    //}

            //}
            //else 
            //{
            //    //up or down
            //    if (y < 0)
            //    {
            //        swipeDown = true;
            //        //Debug.Log("Swipe Done");
            //    }

            //    else
            //    {
            //        swipeUp = true;
            //    }

            //}

        }

        if (tap) //&& !onTheMove) 
        {
            GameObject go = Instantiate(extrude, this.transform.position + extrudeOffset, extrude.transform.rotation);
            //go.transform.parent = this.transform;
            go.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //this.transform.parent = go.transform;
            this.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
           // this.transform.position = new Vector3(this.transform.position.x, go.transform.position.y, this.transform.position.z);
        }
    }

    //private void DestroyExtrude() 
    //{
    //    if (this.transform.childCount > 0) 
    //    {
    //        Destroy(this.transform.GetChild(0).gameObject);
    //    }
    //    else 
    //    {
    //        return;
    //    }
    //}

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
        swipeDown = tap =false;
        //onTheMove = false;
        


        // tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
    }


}//class
