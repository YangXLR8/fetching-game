using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ballPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float pickupRange = 3f; // Range for picking up the ball
    public Transform mouthTransform; // Position to hold the ball

    private GameObject currentBall; // Reference to the currently picked up ball
 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        if (movement != Vector3.zero)
            transform.forward = movement.normalized; // Rotate the dog towards the movement direction

        // Check for spacebar input to pick up or drop the ball
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentBall == null)
            {
                PickUpBall();
            }
            else
            {
                DropBall();
            }
        }
        
    }
    void PickUpBall()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Ball")) // Assuming the ball has the "Ball" tag
            {
                currentBall = collider.gameObject;
                PickUp();
                print("Dog picked up the ball.");
                break; // Exit loop after picking up the first ball
            }
        }
    }

    void DropBall()
    {
        if (currentBall != null)
        {
            currentBall.transform.parent = null; // Detach the ball from the player
            currentBall = null; // Reset the reference to the current ball
        }
        print("Dog dropped up the ball.");
    }


    public void PickUp(){
        if (currentBall != null && mouthTransform != null)
        {
            // Position the ball relative to the mouth transform
            currentBall.transform.position = mouthTransform.position;

            // Rotate the ball to align with the mouth's rotation
            currentBall.transform.rotation = mouthTransform.rotation;

            // Attach the ball to the dog's mouth
            currentBall.transform.parent = mouthTransform;
        }
    }
}
