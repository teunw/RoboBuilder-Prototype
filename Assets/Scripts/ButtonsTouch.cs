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
    public Material ColorOn;
    public Material ColorOff;
    public string Method;
    public GameObject[] Objects;
    public ButtonsTouchEditor OnButtonTouched;
    
    private bool _turnedOn = false;

    public override void OnInteractableObjectTouched(InteractableObjectEventArgs e)
    {
        base.OnInteractableObjectTouched(e);

        foreach (var obj in Objects)
        {
            obj.GetComponent<Renderer>().material = _turnedOn ? ColorOn : ColorOff;
        }

        _turnedOn = !_turnedOn;
        OnButtonTouched.OnTouched.Invoke();
    }
}
[Serializable]
public class ButtonsTouchEditor
{
    public UnityEvent OnTouched;
}