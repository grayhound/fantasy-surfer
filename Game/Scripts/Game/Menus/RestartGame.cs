using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour {
    public GameObject menuCanvasObject;
    public CanvasToggler canvasToggler;

    void Start()
    {
    }

    void Update()
    {
    }

    public void ShowMenuCanvas()
    {
        canvasToggler.ShowMainCanvas(menuCanvasObject);
    }

    public void HideMenuGameCanvas()
    {
        canvasToggler.HideMainCanvas(menuCanvasObject);
    }
}
