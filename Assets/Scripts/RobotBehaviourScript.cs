using System;
using System.Linq;
using UnityEngine;
using VRTK;

public abstract class RobotBehaviourScript : MonoBehaviour
{
    [HideInInspector] public GameObject Cube;

    [ShowInRobot] public bool Enabled = true;

    public Receiver Receiver;

    void Start()
//	private void Start()
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

    public virtual void OnBehaviourTriggered()
    {
    }

    public virtual void Copy<T>(ref T copy) where T : RobotBehaviourScript
    {
        copy.Cube = Cube;
    }

    public virtual void Done()
    {
        Receiver.ScriptDone();
    }
}

/// <summary>
/// Attribute to make sure this variable can be shown in the game
/// </summary>
public class ShowInRobot : Attribute
{
}