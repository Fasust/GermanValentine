using UnityEngine;

public class swipe : MonoBehaviour
{

    public bool tap, swipeUp, swipeDown, swipeRight, swipeLeft;
    public Vector2 startTouche, swipeDelta;
    public int minSwpieDistance = 125;

    private void Update()
    {
        //Resets
        tap = false;
        swipeUp = false;
        swipeDown = false;
        swipeRight = false;
        swipeLeft = false;

        #region Destop Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouche = Input.mousePosition;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }
        #endregion
        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouche = Input.touches[0].position;

            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }
        #endregion

        //Calculate Swipe Distance
        swipeDelta = Vector2.zero;
        if (startTouche != Vector2.zero)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouche;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouche;
            }
        }

        //Cross Swipe Bounds
        if (swipeDelta.magnitude > minSwpieDistance)
        {
            //Swiped
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0)
                {
                    //Left
                    swipeLeft = true;
                }
                else
                {
                    //Right
                    swipeRight = true;
                }
            }
            else
            {
                //Up or Down
                if (y < 0)
                {
                    //Down
                    swipeDown = true;
                }
                else
                {
                    //Up
                    swipeUp = true;
                }
            }

            Reset();
        }

    }
    private void Reset()
    {
        startTouche = Vector2.zero;
        swipeDelta = Vector2.zero;

    }
}
