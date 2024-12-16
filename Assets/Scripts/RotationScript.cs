using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    // Velocidad de rotación del objeto
    float VelocidadDeRotacion = 10;

    // Escalas mínima y máxima permitidas para el objeto
    public float EscalaMin = 0.05f;
    public float EscalaMax = 2f;

    // Velocidad de escala, se ha reducido para hacer la escala más lenta
    public float VelocidadDeEscala = 0.002f;

    // Update is called once per frame
    void Update()
    {
        // Si hay un solo toque en la pantalla y el toque se está moviendo
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Obtenemos el movimiento del dedo (deltaPosition)
            Vector2 PosicionTorque = Input.GetTouch(0).deltaPosition;

            // Calculamos el ángulo de rotación en el eje X y Y basado en el movimiento del dedo
            float x = PosicionTorque.x * Mathf.Deg2Rad * VelocidadDeRotacion;
            float y = PosicionTorque.y * Mathf.Deg2Rad * VelocidadDeRotacion;

            // Obtenemos la posición actual del objeto
            Vector3 objectPosition = transform.position;

            // Rotamos el objeto alrededor de su posición en el eje Y (horizontal)
            transform.RotateAround(objectPosition, Vector3.up, -x);

            // Rotamos el objeto alrededor de su posición en el eje X (vertical)
            transform.RotateAround(objectPosition, Vector3.right, y);
        }
        // Si hay dos toques en la pantalla (pellizco para hacer zoom)
        else if (Input.touchCount == 2)
        {
            // Obtenemos la información de los dos toques
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Calculamos la posición anterior de los toques antes de moverse
            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            // Calculamos la distancia entre los dos toques antes del movimiento
            float distanciaAnterior = Vector2.Distance(touch0PrevPos, touch1PrevPos);

            // Calculamos la distancia actual entre los dos toques
            float distanciaActual = Vector2.Distance(touch0.position, touch1.position);

            // Calculamos el cambio de escala basado en la diferencia entre las distancias
            float factorDeEscala = (distanciaActual - distanciaAnterior) * VelocidadDeEscala;

            // Calculamos la nueva escala del objeto sumando el cambio en cada eje
            Vector3 nuevaEscala = transform.localScale + Vector3.one * factorDeEscala;

            // Limitamos la nueva escala dentro de los valores de escala mínima y máxima
            nuevaEscala.x = Mathf.Clamp(nuevaEscala.x, EscalaMin, EscalaMax);
            nuevaEscala.y = Mathf.Clamp(nuevaEscala.y, EscalaMin, EscalaMax);
            nuevaEscala.z = Mathf.Clamp(nuevaEscala.z, EscalaMin, EscalaMax);

            // Aplicamos la nueva escala al objeto
            transform.localScale = nuevaEscala;
        }
    }
}

