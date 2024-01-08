using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float topClamp = -90f;
    [SerializeField] private float bottomClamp = 90f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    
    private void Start()
    {
        // Locking the cursor to the middle of the screen and making it invisible 
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        GetMouseInput();
    }

    private void GetMouseInput()
    {
        // Getting the mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        //Rotation around the x axis(lookup and down)
        xRotation -= mouseY;
        
        // Clamp Rotation
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);
        
        //Rotation around the y axis(lookleft and right)
        yRotation += mouseX;
        
        transform.parent.localRotation = Quaternion.Euler(xRotation, yRotation , 0f);
    }
}
