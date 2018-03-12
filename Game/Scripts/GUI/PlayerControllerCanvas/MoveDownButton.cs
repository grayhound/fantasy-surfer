using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownButton : MonoBehaviour {
    public PlayerController player_controller;

    public void OnPointerDownAction()
    {
        player_controller.MoveDown();
    }

    public void OnPointerUpAction()
    {
        player_controller.MoveStop();
    }
}
