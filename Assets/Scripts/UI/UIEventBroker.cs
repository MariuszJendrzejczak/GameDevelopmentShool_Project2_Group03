using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventBroker
{
    public static event Action SelectedUnit;
    public static void CallUnitSelected()
    {
        if (SelectedUnit != null)
        {
            SelectedUnit();
        }
    }
    public static event Action AtackedUnit;
    public static void CallAtackedUnit()
    {
        if (AtackedUnit != null)
        {
            AtackedUnit();
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

    public static event Action HumanMove;
    public static void CallHumanMove()
    {
        if (HumanMove != null)
        {
            HumanMove();
        }
    }

    public static event Action ElfesMove;
    public static void CallElfesMove()
    {
        if (ElfesMove != null)
        {
            ElfesMove();
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

    public static event Action HumanSkill;
    public static void CallHumanSkill()
    {
        if (HumanSkill != null)
        {
            HumanSkill();
        }
    }
    public static event Action ElfesSkill;
    public static void CallElfesSkill()
    {
        if (ElfesSkill != null)
        {
            ElfesSkill();
        }
    }

    public static event Action<int> UIHumanScore;
    public static void CallUIHumanScore(int value)
    {
        if (UIHumanScore != null)
        {
            UIHumanScore(value);
        }
    }
    public static event Action<int> UIElfesScore;
    public static void CallUIElfesScore(int value)
    {
        if (UIElfesScore != null)
        {
            UIElfesScore(value);
        }
    }

    public static event Action ElfChosedUnitPanel;
    public static void CallElfChosedUnitPanel()
    {
        if (ElfChosedUnitPanel != null)
        {
            ElfChosedUnitPanel();
        }
    }
    public static event Action HumanChosedUnitPanel;
    public static void CallHumanChosedUnitPanel()
    {
        if (HumanChosedUnitPanel != null)
        {
            HumanChosedUnitPanel();
        }
    }

    public static event Action<GameObject> ShowHumanUnitsChoosed;
    public static void CallShowHumanUnitsChoosed(GameObject humans)
    {
        if (ShowHumanUnitsChoosed != null)
        {
            ShowHumanUnitsChoosed(humans);
        }
    }
    public static event Action<GameObject> ShowElfesUnitsChoosed;
    public static void CallShowElfesUnitsChoosed(GameObject elfes)
    {
        if (ShowElfesUnitsChoosed != null)
        {
            ShowElfesUnitsChoosed(elfes);
        }
    }
}
