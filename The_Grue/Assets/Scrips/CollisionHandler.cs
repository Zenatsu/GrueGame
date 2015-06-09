using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {

    RaycastHit2D torchBoundary;
    Collider2D otherCollider;
    float distance;

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            otherCollider = collider;
            if (collider.GetComponent<PlayerMovement>().grabTorch == true)
            { 
                FollowPlayer();
            }
            else
                collider.GetComponent<PlayerMovement>().grabTorch = false;
        }
    }

    void Update()
    {
        if(otherCollider != null)
        {
            distance = Vector3.Distance(otherCollider.transform.position, this.transform.position);
            if (distance >= 1.98f)
            {
                print("OH GOD THE HORROR!");
            }
        }
        
    }

    void FollowPlayer()
    {
        this.transform.position = otherCollider.transform.position;
    }
}
