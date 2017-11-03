using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovementBehaviour : RobotBehaviourScript
{
    public Vector3 MoveLocation;
    public float LerpTime = 2f;

    private float _currentLerp;
    private bool _isBehaviourTriggered;
    private Vector3 _startPosition;

    public void Update()
    {
        if (!_isBehaviourTriggered) return;
        if (_currentLerp >= LerpTime)
        {
            _isBehaviourTriggered = false;
            _currentLerp = 0;
            return;
        }
        
        _currentLerp += Time.deltaTime;
        var lerpPos = Vector3.Lerp(_startPosition, MoveLocation, _currentLerp / LerpTime);
        base.Robot.transform.position = lerpPos;
    }

    public override void OnBehaviourTriggered()
    {
        base.OnBehaviourTriggered();
        _isBehaviourTriggered = true;
        _startPosition = base.Robot.transform.position;
    }
}