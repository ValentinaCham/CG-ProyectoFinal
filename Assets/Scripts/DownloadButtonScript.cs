using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadButtonScript : MonoBehaviour
{
    public string fileName = "file.txt";
    private string localFilePath;

    public void DownloadFile()
    {
        // localFilePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        localFilePath = System.IO.Path.Combine(Application.dataPath, "Files", fileName);
        StartCoroutine(DownloadCoroutine());
    }

    IEnumerator DownloadCoroutine()
    {
        UnityWebRequest www = UnityWebRequest.Get(localFilePath);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError ||
            www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("File downloaded successfully! Content:");
            Debug.Log(www.downloadHandler.text);
        }
    }
}