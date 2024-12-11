using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    float VelocidadDeRotacion = 10;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 PosicionTorque = Input.GetTouch(0).deltaPosition;
            float x = PosicionTorque.x * Mathf.Deg2Rad * VelocidadDeRotacion;
            float y = PosicionTorque.y * Mathf.Deg2Rad * VelocidadDeRotacion;

            // Obtener la posición del objeto para rotarlo alrededor de sí mismo
            Vector3 objectPosition = transform.position;

            // Rotar alrededor de su propia posición (sin moverlo)
            transform.RotateAround(objectPosition, Vector3.up, -x); // Rotación en el eje Y (horizontal)
            transform.RotateAround(objectPosition, Vector3.right, y); // Rotación en el eje X (vertical)
        }


    }
}