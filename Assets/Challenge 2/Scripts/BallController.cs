using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)                          // Handles the collision of the ball with another object
    {
        
        if (collision.gameObject.CompareTag("Ground"))                  // Check if the ball collided with the ground
        {
            // Stop the ball's movement by setting its velocity to zero
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (collision.gameObject.CompareTag("ReceivingArea"))           //Check if the ball entered the receiving area
        {  
            print("Ball is at the receiving area");
            FarmerController farmerController = FindObjectOfType<FarmerController>();   
            farmerController.BallReturned();                            // Call the BallReturned from the farmer controller
        }
        
        if(collision.gameObject.CompareTag("Dog")){                     //Check if the ball collided with the Dog
            print("Dog is near the ball");
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.PickUp();                                  // Call the PickUp from the player controller

        }

    }
}
