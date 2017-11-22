using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using VRTK;

public class ConnectPoint : GrabbableObjectMidair
{
    [Header("Connect Point parameters")] public TrailRenderer Trail;

    public StartCube StartCube;
    public Receiver Receiver;
    public Transform LineParent;
    public GameObject LinePrefab;

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
        return LastGameObject ?? this.StartCube.gameObject;
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

        this.StartCube.Transmitters.Add(_inCurrentTransmitter);
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
        var newGm = Instantiate(this.LinePrefab, this.LineParent);
        if (!newGm.HasComponent<LineRenderer>() || !newGm.HasComponent<LineRendererFollowTargets>())
        {
            throw new Exception("Prefab has to contain a linerenderer and linerendererfollowtargets component");
        }
        var lineRenderer = newGm.GetComponent<LineRenderer>();
        lineRenderer.SetPositions(new[] {gm.transform.position, gm2.transform.position});

        var lineRendererFollower = newGm.GetComponent<LineRendererFollowTargets>();
        lineRendererFollower.GameObjects = new[] {gm, gm2};
    }

    public void MoveToNextObject()
    {
        this.gameObject.transform.parent.transform.parent = _inCurrentTransmitter.gameObject.transform;
        this.gameObject.transform.parent.transform.localPosition = new Vector3(-0.9f, 0f, 0f);
        this.SpawnLineBetween(this.GetLastGameObject().transform, this._inCurrentTransmitter.transform);
        this.ResetLineAndPoint();
    }
}