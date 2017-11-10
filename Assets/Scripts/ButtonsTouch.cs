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

public class ButtonsTouch : VRTK.VRTK_InteractableObject
{
    [Header("Button touch actions")]
    public Material ColorOn;
    public Material ColorOff;
    public GameObject[] Objects;
    public bool StayColorAfterTouch = true;
    public bool TurnedOn = false;
    public ButtonsTouchEditor OnButtonTouched;


    public override void OnInteractableObjectTouched(InteractableObjectEventArgs e)
    {
        base.OnInteractableObjectTouched(e);
        
        TurnOn();
        ApplyColor();
        OnButtonTouched.OnTurnOn.Invoke();
    }

    public override void OnInteractableObjectUntouched(InteractableObjectEventArgs e)
    {
        base.OnInteractableObjectUntouched(e);
        
        if (StayColorAfterTouch) return;
        
        TurnOff();
        ApplyColor();
        OnButtonTouched.OnTurnOff.Invoke();
    }

    public void ApplyColor()
    {
        foreach (var obj in Objects)
        {
            obj.GetComponent<Renderer>().material = TurnedOn ? ColorOn : ColorOff;
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
[Serializable]
public class ButtonsTouchEditor
{
    public UnityEvent OnTurnOn;
    public UnityEvent OnTurnOff;
}