using System;
using UnityEngine;
using Valve.VR;

public class _MoveForward : RobotBehaviourScript
{
    [ShowInRobot] public float Speed = .1f;

    private Rigidbody _rigidbody;

    private Vector3 startPosition;
    
    private void Start()
    {
        Init();
        _rigidbody = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    void Update()
    {
        
        if (Enabled)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPosition + addVectorAndRotate(), step);
            if (transform.position == startPosition + addVectorAndRotate())
            {
                startPosition = transform.position;
                Done();
            }
//            _rigidbody.velocity = transform.forward * speed * Time.deltaTime;
//
//            Current += transform.forward * speed * Time.deltaTime;
//            if (Current.x >= Total.x && Current.y >= Total.y && Current.z >= Total.z)
//            {
//                Current = new Vector3();
//                Done();
//            }
        }
    }

    /// <summary>
    /// todo : make this not hardcoded
    /// </summary>
    /// <returns></returns>
    Vector3 addVectorAndRotate()
    {
        switch ((int) transform.rotation.eulerAngles.y)
        {
            case 90:
                return new Vector3(-1,0,0);
            case 180:
                return new Vector3(0,0,1);
            case 270:
                return new Vector3(1,0,0);
            case 0:
                return new Vector3(0, 0, -1);
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