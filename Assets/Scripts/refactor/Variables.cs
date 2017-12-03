using System;
using System.Linq;
using UnityEngine;
using VRTK;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;


public class Variables : MonoBehaviour
{
    public Transform sliderPrefab;
    public Transform checkbox;

    void Start()
    {
        var player = GameObject.FindGameObjectsWithTag("Player").First();
        var script = player.GetComponents<RobotBehaviourScript>().First();
        var fields = script.GetFields();

        foreach (var field in fields)
        {
            if (field.FieldType == typeof(Vector3))
            {
                var slider = Instantiate(sliderPrefab);
                slider.SetParent(transform);
                slider.localScale = new Vector3(1, 1, 1);

                Slider sliderScript = slider.gameObject.GetComponent<Slider>();
                var field1 = field;
                sliderScript.onValueChanged.AddListener(value =>
                {
                    Debug.Log("setfield called");
                    script.SetField(field1, new Vector3(value, 0, 0)); // todo : this only sets the x
                });
            }
            if (field.FieldType == typeof(bool))
            { 
                var checkbox = Instantiate(this.checkbox);
                checkbox.SetParent(transform);
                checkbox.localScale = new Vector3(1,1,1);
                var toggle = checkbox.gameObject.GetComponent<Toggle>();
                var field1 = field;
                toggle.onValueChanged.AddListener(value =>
                {
                    script.SetField(field1, value);
                });
            }
        }


//        
////        Transform sliderObject = Instantiate(prefab);
////        var slider =sliderObject.GetComponentInChildren<VRTK_Slider>();
////        Destroy(slider.GetComponentInChildren<ConfigurableJoint>());
////        slider.connectedTo = GameObject.Find("RightController");
//
//        
//        sliderObject.SetParent(transform);
//        
//        slider.ValueChanged += delegate(object sender, Control3DEventArgs args)
//        {
//            Debug.Log("normalised : " + args.normalizedValue);
//            Debug.Log("value : " + args.value);
////            switch (axis.ToLower())
////            {
////                case "x":
////                    value.x = args.normalizedValue;
////                    break;
////                case "y":
////                    value.y = args.normalizedValue;
////                    break;
////                case "z":
////                    value.z = args.normalizedValue;
////                    break;
////                default:
////                    throw new NullReferenceException("No axis specified");
////            }
//        };
//        
    }

    public void UpdateValue(VRTK_Slider slider, Vector3 value, string axis)
    {
    }
}