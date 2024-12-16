using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadButtonScript : MonoBehaviour
{
    // Nombre del archivo a descargar
    public string fileName = "file.txt";
    
    // Ruta local donde se almacenará el archivo descargado
    private string localFilePath;

    // Método público que se llama cuando el botón de descarga es presionado
    public void DownloadFile()
    {
        // Combina la ruta de la aplicación con la carpeta "Files" y el nombre del archivo
        localFilePath = System.IO.Path.Combine(Application.dataPath, "Files", fileName);
        
        // Inicia la corutina para descargar el archivo
        StartCoroutine(DownloadCoroutine());
    }

    // Corutina que maneja la descarga del archivo de manera asincrónica
    IEnumerator DownloadCoroutine()
    {
        // Crea una solicitud HTTP GET para descargar el archivo desde la ruta local
        UnityWebRequest www = UnityWebRequest.Get(localFilePath);

        // Espera hasta que la solicitud se complete
        yield return www.SendWebRequest();

        // Verifica si hubo algún error de conexión o protocolo
        if (www.result == UnityWebRequest.Result.ConnectionError ||
            www.result == UnityWebRequest.Result.ProtocolError)
        {
            // Si hubo un error, lo muestra en la consola
            Debug.LogError(www.error);
        }
        else
        {
            // Si la descarga fue exitosa, muestra el contenido del archivo descargado
            Debug.Log("File downloaded successfully! Content:");
            Debug.Log(www.downloadHandler.text);
        }
    }
}
