using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 startScale;

    public bool scriptsEnabled = true;
    public int currentScript = 0;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        startScale = transform.localScale;

        NextScript();
        currentScript = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1");
            ResetScripts();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2");
            PauzeScripts();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3");
            StopScripts();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("4");
            NextScript();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            RobotBehaviourScript script = gameObject.GetComponents<RobotBehaviourScript>()[currentScript];
            var fields = GetFields(script);
//            foreach (var field in fields)
//            {   
//                SetField(field,script);
//            }
            
        }
    }

    /// <summary>
    /// Gets all fields on the script that have the attribute ShowInRobot
    /// </summary>
    /// <param name="script"></param>
    /// <returns></returns>
    private List<FieldInfo> GetFields(RobotBehaviourScript script)
    {
        FieldInfo[] allFields = script.GetType().GetFields();
        List<FieldInfo> fields = new List<FieldInfo>();
        foreach (var field in allFields)
        {
            object[] a = field.GetCustomAttributes(true);
            foreach (var attrib in a)
            {
                if (attrib.GetType().Name == typeof(ShowInRobot).Name)
                {
                    fields.Add(field);
                }
            }
        }
        return fields;
    }

    /// <summary>
    /// Sets the value to vector.one this is for now only for testing purposes.
    /// TODO : set it so it accepts all kind of objects
    /// </summary>
    /// <param name="fieldInfo"></param>
    /// <param name="script"></param>
    /// <exception cref="NullReferenceException"></exception>
    private void SetField(FieldInfo fieldInfo, RobotBehaviourScript script)
    {
        if (fieldInfo == null)
        {
            throw new NullReferenceException("fieldInfo null");
        }
        try
        {
            fieldInfo.SetValue(script, new Vector3(1f, 1f, 1f));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    private void NextScript()
    {
        currentScript = (currentScript + 1) % gameObject.GetComponents<RobotBehaviourScript>().Length;
        var scripts = gameObject.GetComponents<RobotBehaviourScript>();
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].enabled = i == currentScript;
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        var transmitter = other.GetComponent<Transmitter>();
        if (transmitter != null) // todo : don't add the same script double
        {
            Type type = Type.GetType(transmitter.BehaviourScript);
            gameObject.AddComponent(type);
        }
    }

    public void PauzeScripts()
    {
        foreach (RobotBehaviourScript script in this.gameObject.GetComponents<RobotBehaviourScript>())
        {
            script.enabled = !scriptsEnabled;
        }
        scriptsEnabled = !scriptsEnabled;
    }

    public void StopScripts()
    {
        if (scriptsEnabled)
        {
            PauzeScripts();
        }
        ResetScripts();
    }

    public void ResetScripts()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        transform.localScale = startScale;
    }
}