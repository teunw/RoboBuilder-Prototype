﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VRTK;

public abstract class RobotBehaviourScript : MonoBehaviour
{
    [HideInInspector] public GameObject Cube;

    [ShowInRobot] 
    public bool Enabled = true;

    public bool IsConnectable = true;

    public Receiver Receiver;

    public Transform slider;

    void Start()
    {
        Init();
    }

    protected void Init()
    {
        Cube = gameObject;
        foreach (var script in GetComponents<RobotBehaviourScript>())
        {
            script.Enabled = false;
        }
        //todo : make sure only one object has the player tag or make a different tag
        var player = GameObject.FindGameObjectsWithTag("Player").First(p => p.name == "Robot");
        Receiver = player.GetComponent<Receiver>();
        if (Receiver == null)
        {
            throw new NullReferenceException("Receiver could not be found");
        }
    }


    public virtual void Copy<T>(ref T copy) where T : RobotBehaviourScript
    {
        copy.Cube = Cube;
    }

    public virtual void Done()
    {
        if (Receiver == null)
        {
            throw new NullReferenceException("Receiver null : Did you enable it on a non receiver ?");
        }
        Receiver.ScriptDone();
    }
    
    /// <summary>
    /// Gets all fields on the script that have the attribute ShowInRobot
    /// </summary>
    /// <param name="script"></param>
    /// <returns></returns>
    public List<FieldInfo> GetFields()
    {
        if (slider != null)
        {

            var slider2 = Instantiate(slider);
            slider2.transform.SetParent(transform);
            slider2.transform.localScale = new Vector3(.2f, .2f, .2f);
        }

        FieldInfo[] allFields = GetType().GetFields();
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
    /// <param name="value"></param>
    /// <exception cref="NullReferenceException"></exception>
    public void SetField(FieldInfo fieldInfo, RobotBehaviourScript script, object value)
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
}

/// <summary>
/// Attribute to make sure this variable can be shown in the game
/// </summary>
public class ShowInRobot : Attribute
{
}