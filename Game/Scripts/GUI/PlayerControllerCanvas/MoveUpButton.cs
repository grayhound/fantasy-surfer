using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpButton : MonoBehaviour {
    public PlayerController player_controller;

    public void OnPointerDownAction()
    {
        player_controller.MoveUp();
    }

    public void OnPointerUpAction()
    {
        player_controller.MoveStop();
    }
}
