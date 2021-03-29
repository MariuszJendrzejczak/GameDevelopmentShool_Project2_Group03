using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventBroker
{
    public static event Action<GameObject> SelectedUnit;
    public static void CallUnitSelected(GameObject unit)
    {
        if (SelectedUnit != null)
        {
            SelectedUnit(unit);
        }
    }
    public static event Action<GameObject> AtackedUnit;
    public static void CallAtackedUnit(GameObject unit)
    {
        if (AtackedUnit != null)
        {
            AtackedUnit(unit);
        }
    }
    public static event Action ShakeCamera;
    public static void CallShakeCamera()
    {
        if (ShakeCamera != null)
        {
            ShakeCamera();
        }
    }
    public static event Action<GameObject> WasAttacked;
    public static void CallWasAttacked(GameObject unit)
    {
        if (WasAttacked != null)
        {
            WasAttacked(unit);
        }
    }

    /*public static event Action<GameObject> CurrentScore;
    public static void CallCurrentScoreScore()
    {
        if (AtackedUnit != null)
        {
            AtackedUnit(unit);
        }
    }*/
}
