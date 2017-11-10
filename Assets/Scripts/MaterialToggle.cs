using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VRTK;

public class MaterialToggle : MonoBehaviour
{
    [Header("Button touch actions")]
    public Material ColorOn;
    public Material ColorOff;
    public GameObject[] Objects;
    public bool TurnedOn = false;

    public void ApplyColor()
    {
        foreach (var obj in Objects)
        {
            obj.GetComponent<Renderer>().material = TurnedOn ? ColorOn : ColorOff;
        }
    }

    public void Toggle()
    {
        if (TurnedOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    public void TurnOff()
    {
        this.TurnedOn = false;
        ApplyColor();
    }

    public void TurnOn()
    {
        this.TurnedOn = true;
        ApplyColor();
    }
}