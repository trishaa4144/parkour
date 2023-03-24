using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    public Transform cam;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Debug.Log($"Horizontal input: {horizontal}, Vertical input: {vertical}");

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        
        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection = Quaternion.LookRotation(cameraForward) * movementDirection;

        float magnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();

        characterController.SimpleMove(movementDirection * magnitude * speed);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
