using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FarmerController : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform throwPosition;
    [SerializeField] float minThrowForce = 5f;
    [SerializeField] float maxThrowForce = 10f;
   
    private GameObject currentBall;
     private GameObject dog; // Reference to the dog GameObject
    private bool hasBall = true; // Indicates whether the farmer has a ball
    
    void Start()
    {   
        dog = GameObject.FindGameObjectWithTag("Dog");          //find the object the Dog
        if (hasBall)
            ThrowBall();
    }
     void Update()
    {
        transform.LookAt(dog.transform);                        // Make the farmer face the location of the dog
    }

    void ThrowBall()
    {   
        if (hasBall && ballPrefab != null)                      // if the farmer has the ball
        {
            currentBall = Instantiate(ballPrefab, throwPosition.position, Quaternion.identity);
            Rigidbody rb = currentBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float throwForce = Random.Range(minThrowForce, maxThrowForce);
                // Generate a random direction within a cone facing upwards
                Vector3 throwDirection = Random.onUnitSphere;
                throwDirection.y = Mathf.Abs(throwDirection.y); // Ensure the direction is upward
                
                // rb.AddForce(throwDirection * throwForce);
                rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            }
            print("Ball is thrown!");

        }
        hasBall = false;
        
    }
   
    public void BallReturned()                   // Call this method when the ball is returned by the dog
    {
        hasBall = true;                          // Farmer has the ball again
        print("Farmer received the ball!");

        if (currentBall != null)                // Check if the current ball exists and destroy it
            Destroy(currentBall);
        
        Invoke("ThrowBall", 5f);                //throw a new ball after 5sec delay
    }
   
}
