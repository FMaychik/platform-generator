using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 2f;

    private void Update()
    {
        // Перемещение камеры
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed * Time.deltaTime;
        transform.Translate(moveDirection);

        // Вращение камеры
        if (Input.GetMouseButton(1)) // Правая кнопка мыши нажата
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 rotationAmount = new Vector3(-mouseY, mouseX, 0f) * rotationSpeed;
            transform.Rotate(rotationAmount);
        }
    }
}
