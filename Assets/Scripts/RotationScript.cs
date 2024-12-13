using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    float VelocidadDeRotacion = 10;
    public float EscalaMin = 0.05f;
    public float EscalaMax = 2f;
    public float VelocidadDeEscala = 0.002f; // Reducción de la velocidad de escala para hacerla más lenta

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 PosicionTorque = Input.GetTouch(0).deltaPosition;
            float x = PosicionTorque.x * Mathf.Deg2Rad * VelocidadDeRotacion;
            float y = PosicionTorque.y * Mathf.Deg2Rad * VelocidadDeRotacion;

            Vector3 objectPosition = transform.position;

            transform.RotateAround(objectPosition, Vector3.up, -x);
            transform.RotateAround(objectPosition, Vector3.right, y);
        }
        else if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Calcular la distancia anterior y actual entre los dedos
            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            float distanciaAnterior = Vector2.Distance(touch0PrevPos, touch1PrevPos);
            float distanciaActual = Vector2.Distance(touch0.position, touch1.position);

            // Calcular el cambio de escala
            float factorDeEscala = (distanciaActual - distanciaAnterior) * VelocidadDeEscala;

            // Ajustar la nueva escala
            Vector3 nuevaEscala = transform.localScale + Vector3.one * factorDeEscala;

            // Limitar la escala dentro de los valores mínimo y máximo
            nuevaEscala.x = Mathf.Clamp(nuevaEscala.x, EscalaMin, EscalaMax);
            nuevaEscala.y = Mathf.Clamp(nuevaEscala.y, EscalaMin, EscalaMax);
            nuevaEscala.z = Mathf.Clamp(nuevaEscala.z, EscalaMin, EscalaMax);

            transform.localScale = nuevaEscala;
        }
    }
}
