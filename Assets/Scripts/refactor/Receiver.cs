using System;
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
            foreach (var field in fields)
            {
                if (field.Name == "Step")
                {
                    SetField(field,script);
                }
            }
        }
    }

    private FieldInfo[] GetFields(RobotBehaviourScript script)
    {
        FieldInfo[] rProps = script.GetType().GetFields();
        String[] fields = new String[rProps.Length];
        for (int i = 0; i < rProps.Length; i++)
        {
            fields[i] = rProps[i].Name;
            Debug.Log(rProps[i].Name);
        }
        return rProps;
    }

    private void SetField(FieldInfo fieldInfo, RobotBehaviourScript script)
    {
        if (fieldInfo == null)
        {
            throw new NullReferenceException("fieldInfo null"); 
        }
        try
        {
            fieldInfo.SetValue(script,new Vector3(1f,1f,1f));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    private void NextScript()
    {     
        currentScript = (currentScript+1)% gameObject.GetComponents<RobotBehaviourScript>().Length;
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