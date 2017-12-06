using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using VRTK;

public class ConnectPoint : GrabbableObjectMidair
{
    [Header("Connect Point parameters")] public TrailRenderer Trail;

    public Receiver Receiver;
    public Transform LineParent;
    public LineRendererFollowTargets LinePrefab;

    private Transmitter _inCurrentTransmitter = null;
    private float TrailTime;
    private GameObject LastGameObject;

    public override void Start()
    {
        base.Start();
        this.TrailTime = this.Trail.time;
        this.Trail.gameObject.SetActive(false);
    }

    public GameObject GetLastGameObject()
    {
        return LastGameObject;
    }

    public void NullifyInCurrentTransmitter()
    {
        this.LastGameObject = this._inCurrentTransmitter.gameObject;
        this._inCurrentTransmitter = null;
    }

    public virtual void EnableTrail()
    {
        Trail.gameObject.SetActive(true);
    }

    public virtual IEnumerator ResetTrailAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        ResetTrailTime();
        this.Trail.gameObject.SetActive(false);
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
        this.EnableTrail();
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
        if (!this._inCurrentTransmitter.BehaviourScript.IsConnectable)
        {
            Debug.Log("Transmitter not connectable");
            ResetLineAndPoint();
            return;
        }

        this.Receiver.AddScript(this._inCurrentTransmitter);
        ResetLineAndPoint();
        MoveToNextObject();
        Debug.Log("Added " + _inCurrentTransmitter.BehaviourScript);
        NullifyInCurrentTransmitter();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.HasComponent<Transmitter>()) return;

        this._inCurrentTransmitter = other.gameObject.GetComponent<Transmitter>();
        Debug.Log("Behaviour set");
    }

    public virtual void OnCollisionExit(Collision other)
    {
        if (_inCurrentTransmitter == null || other.gameObject != _inCurrentTransmitter.gameObject) return;
        _inCurrentTransmitter = null;
        Debug.Log("Behaviour left");
    }

    public void SpawnLineBetween(Transform gm, Transform gm2)
    {
        Debug.Log("Spawning line");
        var newGm = Instantiate(this.LinePrefab.gameObject, this.LineParent);
        newGm.GetComponent<LineRendererFollowTargets>().GameObjects = new[] {gm, gm2};
    }

    public void MoveToNextObject()
    {
        this.gameObject.transform.parent.transform.parent = _inCurrentTransmitter.gameObject.transform;
        this.gameObject.transform.parent.transform.localPosition = new Vector3(-1.5f, 0f, 0f);
        this.gameObject.transform.parent.transform.localScale = new Vector3(.4f, .4f, .4f);
        if (this.GetLastGameObject() != null)
        {
            this.SpawnLineBetween(this.GetLastGameObject().transform, this._inCurrentTransmitter.transform);
        }
        this.ResetLineAndPoint();
    }
}