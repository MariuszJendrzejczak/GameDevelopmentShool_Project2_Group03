﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CMEventBroker
{
    public static event Action ChangeGameMode;
    public static void CallChangeGameMode()
    {
        if (ChangeGameMode != null)
        {
            ChangeGameMode();
        }
    }

    public static event Action ChangeGameState;
    public static void CallChangeGameState()
    {
        if (ChangeGameState != null)
        {
            ChangeGameState();
        }
    }
    public static event Action EndCurrentState;
    public static void CallEndCurrentState()
    {
        if (ChangeGameState != null)
        {
            EndCurrentState();
        }
    }
}
