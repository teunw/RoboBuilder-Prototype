using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Receiver : MonoBehaviour
{
    public Vector3 startPos;
    public Quaternion startRot;
    public Vector3 startScale;
    
    public List<RobotBehaviourScript> BehaviourScripts = new List<RobotBehaviourScript>();
    public bool scriptsEnabled = true;
    
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        startScale = transform.localScale;
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
    }

    public void OnTriggerEnter(Collider other)
    {
        var transmitter = other.GetComponent<Transmitter>();
        if (transmitter != null) // todo : don't add the same script double
        {
            Type type = Type.GetType(transmitter.BehaviourScript);
            gameObject.AddComponent(type);
            BehaviourScripts.Add((RobotBehaviourScript) gameObject.GetComponent(type));
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