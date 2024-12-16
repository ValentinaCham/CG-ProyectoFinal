using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.LegacyInputHelpers;

public class MultiTargetsManager : MonoBehaviour
{
    // Referencia al ARTrackedImageManager para gestionar las imágenes rastreadas
    [SerializeField] private ARTrackedImageManager aRTrackedImageManager;

    // Array de modelos AR a colocar en el mundo
    [SerializeField] private GameObject[] aRModelsToPlace;

    // Diccionario para almacenar los modelos AR con su nombre como clave
    private Dictionary<string, GameObject> aRModels = new Dictionary<string, GameObject>();

    // Diccionario para llevar el estado de activación de cada modelo AR (si está visible o no)
    private Dictionary<string, bool> modelState = new Dictionary<string, bool>();

    // Se llama al inicio para inicializar los modelos AR
    void Start()
    {
        // Instancia los modelos AR y los agrega a los diccionarios
        foreach (var aRModel in aRModelsToPlace)
        {
            // Instancia cada modelo AR en la posición (0, 0, 0) sin rotación
            GameObject newARModel = Instantiate(aRModel, Vector3.zero, Quaternion.identity);

            // Asigna el nombre del modelo AR al GameObject instanciado
            newARModel.name = aRModel.name;

            // Agrega el modelo AR al diccionario aRModels con su nombre como clave
            aRModels.Add(newARModel.name, newARModel);

            // Desactiva el modelo AR inicialmente
            newARModel.SetActive(false);

            // Inicializa el estado del modelo como desactivado (false)
            modelState.Add(newARModel.name, false);
        }
    }

    // Se llama cuando el objeto se habilita
    private void OnEnable()
    {
        // Se suscribe al evento que se dispara cuando cambian las imágenes rastreadas
        aRTrackedImageManager.trackedImagesChanged += ImageFound;
    }

    // Se llama cuando el objeto se deshabilita
    private void OnDisable()
    {
        // Se desuscribe del evento cuando cambian las imágenes rastreadas
        aRTrackedImageManager.trackedImagesChanged -= ImageFound;
    }

    // Este método se llama cada vez que se detecta un cambio en las imágenes rastreadas
    private void ImageFound(ARTrackedImagesChangedEventArgs eventData)
    {
        // Itera sobre las imágenes que se han agregado recientemente
        foreach (var trackedImage in eventData.added)
        {
            // Muestra el modelo AR asociado a la imagen rastreada
            ShowARModel(trackedImage);
        }

        // Itera sobre las imágenes que han sido actualizadas
        foreach (var trackedImage in eventData.updated)
        {
            // Si la imagen está siendo rastreada correctamente, muestra el modelo AR
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                ShowARModel(trackedImage);
            }
            // Si la imagen está parcialmente rastreada, oculta el modelo AR
            else if (trackedImage.trackingState == TrackingState.Limited)
            {
                HideARModel(trackedImage);
            }
        }
    }

    // Muestra el modelo AR correspondiente a la imagen rastreada
    private void ShowARModel(ARTrackedImage trackedImage)
    {
        // Verifica si el modelo ya está activado
        bool isModelActivated = modelState[trackedImage.referenceImage.name];

        if (!isModelActivated)
        {
            // Si el modelo no está activado, activarlo y colocarlo en la posición de la imagen rastreada
            GameObject aRModel = aRModels[trackedImage.referenceImage.name];
            aRModel.transform.position = trackedImage.transform.position;
            aRModel.SetActive(true);

            // Actualiza el estado del modelo a activado
            modelState[trackedImage.referenceImage.name] = true;
        }
        else
        {
            // Si el modelo ya está activado, solo actualiza su posición
            GameObject aRModel = aRModels[trackedImage.referenceImage.name];
            aRModel.transform.position = trackedImage.transform.position;
        }
    }

    // Oculta el modelo AR correspondiente a la imagen rastreada
    private void HideARModel(ARTrackedImage trackedImage)
    {
        // Verifica si el modelo está activado
        bool isModelActivated = modelState[trackedImage.referenceImage.name];

        if (isModelActivated)
        {
            // Si el modelo está activado, desactivarlo y actualizar su estado
            GameObject aRModel = aRModels[trackedImage.referenceImage.name];
            aRModel.SetActive(false);
            modelState[trackedImage.referenceImage.name] = false;
        }
    }
}
