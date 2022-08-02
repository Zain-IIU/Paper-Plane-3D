using UnityEngine;

public class SwerveInput : MonoBehaviour
{
    private float lastFrameFromPosX;
    private float moveFactorX;
    
    private bool onScreenHold;
    
    public float MoveFactorX => moveFactorX;

    public bool OnScreenHold
    {
        get => onScreenHold;
        set => onScreenHold = value;
    }


    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            lastFrameFromPosX = Input.mousePosition.x;
            onScreenHold = true;
        } 
        else if (Input.GetMouseButton(0))
        {
            moveFactorX = Input.mousePosition.x - lastFrameFromPosX;
            lastFrameFromPosX = Input.mousePosition.x;
        } 
        else if (Input.GetMouseButtonUp(0))
        {
            moveFactorX = 0f;
            onScreenHold = false;
        }
#endif

#if UNITY_ANDROID
        if (Input.touches.Length > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                lastFrameFromPosX = Input.GetTouch(0).position.x;
                onScreenHold = true;
            } 
            else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                moveFactorX = Input.GetTouch(0).position.x - lastFrameFromPosX;
                lastFrameFromPosX = Input.GetTouch(0).position.x;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                moveFactorX = 0f;
                onScreenHold = false;
            }
        }
#endif

    }
}
