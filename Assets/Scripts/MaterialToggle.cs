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
    [Header("Button touch actions")] public Material ColorOn;
    public Material ColorOff;
    public GameObject[] Objects = new GameObject[0];
    public bool TurnedOn = false;

    private void Start()
    {
        if (Objects == null)
        {
            Objects = new[] {gameObject};
        }
    }

    public void ApplyColor()
    {
        foreach (var obj in Objects)
        {
            obj.GetComponent<Renderer>().material = TurnedOn ? ColorOn : ColorOff;
        }
    }
    
    public IEnumerator WaitTurnOnCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.TurnOn();
    }

    public IEnumerator WaitTurnOffCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.TurnOff();
    }
    
    public void WaitTurnOn(float time)
    {
        StartCoroutine(WaitTurnOffCoroutine(time));
    }

    public void WaitTurnOff(float time)
    {
        StartCoroutine(WaitTurnOffCoroutine(time));
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