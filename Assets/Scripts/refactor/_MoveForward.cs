﻿using System;
using UnityEngine;
using Valve.VR;

public class _MoveForward : RobotBehaviourScript
{
    [ShowInRobot] public float Speed = 1f;

    private Rigidbody _rigidbody;

    private Vector3 startPosition;
    
    private void Start()
    {
        Init();
        _rigidbody = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }
    private bool _wasDisabled = true;
    void Update()
    {
        if (!Enabled)
        {
            _wasDisabled = true;
        }
        if (Enabled)
        {
            if (_wasDisabled)
            {
                startPosition = transform.position;
                _wasDisabled = false;
            }
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPosition + AddVectorAndRotate(), step);
            if (transform.position == startPosition + AddVectorAndRotate())
            {
                startPosition = transform.position;
                Done();
            }
        }
    }

    /// <summary>
    /// todo : make this not hardcoded
    /// </summary>
    /// <returns></returns>
    Vector3 AddVectorAndRotate()
    {
        switch ((int) transform.rotation.eulerAngles.y)
        {
            case 90:
                return new Vector3(1,0,0);
            case 180:
                return new Vector3(0,0,-1);
            case 270:
                return new Vector3(-1,0,0);
            case 0:
                return new Vector3(0, 0, 1);
            default:
                throw new Exception("aaaaaaa");
        }
    }

    public override string ToString()
    {
        return name;
    }

    public override void Copy<T>(ref T copyO)
    {
        base.Copy(ref copyO);
        var copy = copyO as _MoveForward;
        if (copy != null)
        {
            copy.Speed = Speed;
            copy.Enabled = Enabled;
        }
        else
        {
            throw new NotImplementedException(
                "Somthing went horribly wrong, could not copy the data to a new object. The to copy object");
        }
    }
}