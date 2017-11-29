using System;
using UnityEngine;
using VRTK;

public class Variables : MonoBehaviour
{
    void Start()
    {
        
        
    }

    public void UpdateValue(VRTK_Slider slider, Vector3 value, string axis)
    {
        slider.ValueChanged += delegate(object sender, Control3DEventArgs args)
        {
            Debug.Log("normalised : " + args.normalizedValue);
            Debug.Log("value : " + args.value);

            switch (axis.ToLower())
            {
                case "x":
                    value.x = args.normalizedValue;
                    break;
                case "y":
                    value.y = args.normalizedValue;
                    break;
                case "z":
                    value.z = args.normalizedValue;
                    break;
                default:
                    throw new NullReferenceException("No axis specified");
            }
        };
    }
}