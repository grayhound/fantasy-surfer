﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    public Tutorial tutorial;

    public void OnClickAction()
    {
        tutorial.ShowPanel();
    }
}
