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
//                SetField(field,script,new Vector3(1f, 1f, 1f));
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
    private void SetField(FieldInfo fieldInfo, RobotBehaviourScript script, object value)
    {
        if (fieldInfo == null)
        {
            throw new NullReferenceException("fieldInfo null");
        }
        try
        {
            fieldInfo.SetValue(script, value);
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
        if (GetComponents<RobotBehaviourScript>().Length == 0)
        {
            return;
        }
        // set the currentscript to the index of the start when a end of a loop is hit
        currentScript = ifForLoop();
        SetNext();
    }

    private int ifForLoop()
    {
        int currentAddOne = (currentScript + 1) % GetComponents<RobotBehaviourScript>().Length;
        var script = GetComponents<RobotBehaviourScript>()[currentAddOne] as _Loop;
        if (script == null) return currentScript;
        if (!script.start && !script.EndOfLoop())
        {
            currentScript = GetIndexOfScript(script.other);
            return ifForLoop();
        }
        currentScript = (currentScript + 1) % GetComponents<RobotBehaviourScript>().Length;
        return ifForLoop();
    }

    private void SetNext()
    {
        RobotBehaviourScript[] scripts;
        currentScript = (currentScript + 1) % GetComponents<RobotBehaviourScript>().Length;
        scripts = GetComponents<RobotBehaviourScript>();
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
        Debug.Log("trigger called");
        var transmitter = other.GetComponent<Transmitter>();
        AddScript(transmitter);
    }

    public void AddScript(Transmitter transmitter)
    {
        if (transmitter != null) // todo : don't add the same script double
        {
            RobotBehaviourScript script =
                (RobotBehaviourScript) gameObject.AddComponent(transmitter.BehaviourScript.GetType());
            transmitter.BehaviourScript.Copy(ref script);
            // todo : make it so this doesn;t have to happen
            transmitter.enabled = true;
        }
        
    }

    /// <summary>
    /// Pauzes all scripta
    /// todo : make sure only the current script is played
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