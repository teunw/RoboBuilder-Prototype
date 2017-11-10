using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;

public class ConnectPoint : GrabbableObjectMidair
{
    [Header("Connect Point parameters")] 
    public TrailRenderer Trail;
    public ConnectPointManager ConnectPointManager;

    private Transmitter _inCurrentTransmitter = null;
    private float TrailTime;

    public override void Start()
    {
        base.Start();
        this.TrailTime = this.Trail.time;
        this.Trail.gameObject.SetActive(false);
    }

    public virtual void DisableAllPointsExceptThis()
    {
        Trail.gameObject.SetActive(true);
        ConnectPointManager.DisableExcept(this);
    }

    public virtual IEnumerator ResetTrailAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        ResetTrailTime();
    }

    public virtual void ResetTrailTime()
    {
        this.Trail.time = this.TrailTime;
    }

    public virtual void ResetLineAndPoint()
    {
        this.transform.localPosition = new Vector3(0f, 0f, 0f);
        this.Trail.time = 0f;
        StartCoroutine(ResetTrailAfterTime(.5f));
    }

    public override void OnInteractableObjectGrabbed(InteractableObjectEventArgs e)
    {
        base.OnInteractableObjectGrabbed(e);
        this.DisableAllPointsExceptThis();
    }

    public override void OnInteractableObjectUngrabbed(InteractableObjectEventArgs e)
    {
        base.OnInteractableObjectUngrabbed(e);

        if (_inCurrentTransmitter == null)
        {
            Debug.Log("Resetting line and point");
            ResetLineAndPoint();
            return;
        }

        if (_inCurrentTransmitter.HasComponent<VRTK_BaseHighlighter>())
        {
//            _inCurrentTransmitter.GetComponent<VRTK_BaseHighlighter>().Unhighlight();
        }        
        this.ConnectPointManager.StartCube.Transmitters.Add(_inCurrentTransmitter);
        this.ConnectPointManager.Receiver.AddScript(this._inCurrentTransmitter);
        ResetLineAndPoint();
        Debug.Log("Added " + _inCurrentTransmitter.BehaviourScript);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.HasComponent<Transmitter>()) return;

        this._inCurrentTransmitter = other.gameObject.GetComponent<Transmitter>();
        Debug.Log("Behaviour set");
        if (_inCurrentTransmitter.HasComponent<VRTK_BaseHighlighter>())
        {
//            _inCurrentTransmitter.GetComponent<VRTK_BaseHighlighter>().Highlight();
            Debug.Log("Highlighting");
        }
    }

    public virtual void OnCollisionExit(Collision other)
    {
        if (_inCurrentTransmitter == null || other.gameObject != _inCurrentTransmitter.gameObject) return;
        _inCurrentTransmitter = null;
        Debug.Log("Behaviour left");
    }    
}