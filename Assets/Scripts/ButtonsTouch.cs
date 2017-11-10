using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ButtonsTouch : VRTK.VRTK_InteractableObject { 

    public Material colorOn;
    public Material colorOff;

    public GameObject [] Objects;

    bool turnedOn = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnInteractableObjectTouched(InteractableObjectEventArgs e)
    {
        base.OnInteractableObjectTouched(e);

        foreach (var obj in Objects)
        {
            obj.GetComponent<Renderer>().material = turnedOn ? colorOn : colorOff;
        }
        
        turnedOn = !turnedOn;

    }
}
