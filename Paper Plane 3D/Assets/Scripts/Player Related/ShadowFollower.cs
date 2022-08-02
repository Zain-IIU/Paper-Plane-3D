using UnityEngine;

public class ShadowFollower : MonoBehaviour
{
    public Transform objectToFollow;  // Player in your Case
    
     
    void Update () {
        // Create a new Vector 3 with the positions of object to follow. Substract offset from pos.z
        Vector3 myNewPos = new Vector3(transform.position.x, transform.position.y, objectToFollow.position.z);
 
        // Set position of the scripts GameObject to the previous created postition
        transform.position = myNewPos;
    }
}
