using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToggler : MonoBehaviour
{
    public ExitGame exitGame;
    public GameObject pleaseWaitCanvas;

    public GameObject[] canvasObjects;
    private Dictionary<GameObject, Dictionary<GameObject, bool>> canvasObjectsState = new Dictionary<GameObject, Dictionary<GameObject, bool>>();

    public void ShowPleaseWait()
    {
        exitGame.Disable();
        ShowMainCanvas(pleaseWaitCanvas);
    }

    public void HidePleaseWait()
    {
        exitGame.Enable();
        HideMainCanvas(pleaseWaitCanvas);
    }

    public void ShowMainCanvas(GameObject mainCanvas)
    {
        mainCanvas.SetActive(true);

        if (canvasObjectsState.ContainsKey(mainCanvas)) {
            canvasObjectsState.Remove(mainCanvas);
        }
        canvasObjectsState.Add(mainCanvas, new Dictionary<GameObject, bool>());

        foreach (GameObject canvasObject in canvasObjects) {                 
            if (canvasObject != mainCanvas) {
                canvasObjectsState[mainCanvas].Add(canvasObject, canvasObject.activeSelf);
                canvasObject.SetActive(false);
            }
        }

    }

    public void HideMainCanvas(GameObject mainCanvas)
    {        
        if (!canvasObjectsState.ContainsKey(mainCanvas)) {
            return;
        }
        mainCanvas.SetActive(false);
        for (int i = 0; i < canvasObjects.Length; i++) {            
            GameObject canvasObject = canvasObjects[i];
            if (canvasObject == mainCanvas) {                                
                continue;
            }
            
            bool canvasObjectState = canvasObjectsState[mainCanvas][canvasObject];
            if (canvasObjectState) {
                canvasObject.SetActive(true);
            }
        }
        canvasObjectsState.Remove(mainCanvas);        
    }
}
