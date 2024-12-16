using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Método público que permite cargar una escena por su nombre
    public void loadScene(string sceneName)
    {
        // Carga la escena especificada por el nombre
        SceneManager.LoadScene(sceneName);

        // Muestra un mensaje en la consola para indicar que se ha llamado a la función
        Debug.Log("Gaaa");
    }
}
