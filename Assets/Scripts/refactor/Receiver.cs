using System;
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

    /// <summary>
    /// Method that moves the currently executing script
    /// </summary>
    private void NextScript()
    {
        // todo : make sure loops in loops are possible
        // set the currentscript to the index of the start when a end of a loop is hit
        var scriptNew = GetComponents<RobotBehaviourScript>()[currentScript + 1] as _Loop;
        if (scriptNew != null &&
            !scriptNew.start)
        {
            if (!scriptNew.EndOfLoop())
            {
                currentScript = GetIndexOfScript(scriptNew.other);
                return;
            }
        }

        // if its not the end of a for loop
        // go to the next script
        currentScript = (currentScript + 1) % GetComponents<RobotBehaviourScript>().Length;
        var scripts = GetComponents<RobotBehaviourScript>();
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].enabled = i == currentScript;
        }
    }

    /// <summary>
    /// Helper method to get the index of the for loop start
    /// </summary>
    private int GetIndexOfScript(RobotBehaviourScript script)
    {
        for (int i = 0; i < GetComponents<RobotBehaviourScript>().Length; i++)
        {
            if (GetComponents<RobotBehaviourScript>()[i] == script)
            {
                return i;
            }
        }
        throw new KeyNotFoundException();
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

    /// <summary>
    /// Pauzes all scripta
    /// </summary>
    public void PauzeScripts()
    {
        foreach (RobotBehaviourScript script in this.gameObject.GetComponents<RobotBehaviourScript>())
        {
            script.enabled = !scriptsEnabled;
        }
        scriptsEnabled = !scriptsEnabled;
    }

    /// <summary>
    /// Pauses all scripts and resets it
    /// </summary>
    public void StopScripts()
    {
        if (scriptsEnabled)
        {
            PauzeScripts();
        }
        ResetScripts();
    }

    /// <summary>
    /// Resets the scripts to the starting position, rotation and scale
    /// </summary>
    public void ResetScripts()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        transform.localScale = startScale;
    }
}