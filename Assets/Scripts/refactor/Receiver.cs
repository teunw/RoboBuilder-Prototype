using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Policy;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    /// <summary>
    /// Variables that keep track of the starting state
    /// </summary>
    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 startScale;

    public bool scriptsEnabled = true;
    
    /// <summary>
    /// Used as the index of the current script
    /// Example : GetComponents<RobotBehaviourScript>()[currentScript]
    /// </summary>
    public int currentScript;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        startScale = transform.localScale;
        
        SetCurrentActive();
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
//            RobotBehaviourScript script = gameObject.GetComponents<RobotBehaviourScript>()[currentScript];
//            var fields = GetFields(script);
//            foreach (var field in fields)
//            {   
//                SetField(field,script,new Vector3(1f, 1f, 1f));
//            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Undo();
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
        currentScript = ForloopLogic();
        increment();
        SetCurrentActive();
    }
    
    /// <summary>
    /// Makes sure so the current script is never the current one
    /// </summary>
    /// <returns></returns>
    private int ForloopLogic()
    {
        int currentAddOne = (currentScript + 1) % GetComponents<RobotBehaviourScript>().Length;
        var script = GetComponents<RobotBehaviourScript>()[currentAddOne] as _Loop;
        if (script == null) return currentScript;
        if (!script.start && !script.EndOfLoop())
        {
            currentScript = GetIndexOfScript(script.other);
            return ForloopLogic();
        }
        increment();
        return ForloopLogic();
    }

    private void increment()
    {
        currentScript = (currentScript + 1) % GetComponents<RobotBehaviourScript>().Length;
    }

    /// <summary>
    /// Goes to the next script and set the corresponding scipts to Enabled and disabled
    /// </summary>
    private void SetCurrentActive()
    {
        RobotBehaviourScript[] scripts;
        //todo : should this have the increment ?
        scripts = GetComponents<RobotBehaviourScript>();
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].Enabled = i == currentScript;
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

    /// <summary>
    /// When the t
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger called");
        if (other.GetComponent<Transmitter>() != null)
        {
//            AddScript(other.GetComponent<Transmitter>());
        }
    }
    /// <summary>
    /// Add a script from a transmitter
    /// </summary>
    /// <param name="transmitter"></param>
    public void AddScript(Transmitter transmitter)
    {
        if (transmitter == null) return;
        var script = (RobotBehaviourScript) gameObject.AddComponent(transmitter.BehaviourScript.GetType());
        transmitter.BehaviourScript.Copy(ref script);
    }

    /// <summary>
    /// Removes the script from the line and returns the next cube that is in line
    /// </summary>
    /// <param name="transmitter"></param>
    /// <returns>the next gameobject in the list</returns>
    public GameObject RemoveScript(Transmitter transmitter)
    {
        var scripts = GetComponents<RobotBehaviourScript>();
        for (int i = 0; i < scripts.Length; i++)
        {
            if (scripts[i].Cube == transmitter.gameObject)
            {
                Destroy(scripts[i]);
                if (GetComponents<RobotBehaviourScript>().Length < 0)
                {
                    return GetComponents<RobotBehaviourScript>()[i].Cube;
                }
            }
        }
        Debug.Log("Could not find any scripts !");
        return null;
    }


    /// <summary>
    /// Pauzes all scripta
    /// todo : make sure only the current script is played
    /// </summary>
    public void PauzeScripts()
    {
        scriptsEnabled = !scriptsEnabled;
        foreach (RobotBehaviourScript script in this.gameObject.GetComponents<RobotBehaviourScript>())
        {
            script.Enabled = scriptsEnabled;
        }
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

    /// <summary>
    /// Clears all the scripts off 
    /// </summary>
    public void ClearScripts()
    {
        foreach (var robotBehaviourScript in gameObject.GetComponents<RobotBehaviourScript>())
        {
            Destroy(robotBehaviourScript);
        }
    }

    /// <summary>
    /// Goes back to the previous script
    /// </summary>
    public void Undo()
    {
        // minus 2 because set next adds one
        currentScript = this.GetComponents<RobotBehaviourScript>().Length -
                        Math.Abs(currentScript - 2 % this.GetComponents<RobotBehaviourScript>().Length);
        SetCurrentActive();
    }

    public void ScriptDone()
    {
        NextScript();
    }
}