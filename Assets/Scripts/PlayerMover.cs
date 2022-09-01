using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _needPush = true;
    
    public Rigidbody Rigidbody => _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Player.Instance.Died += StopPushing;
    }

    private void OnDisable()
    {
        Player.Instance.Died -= StopPushing;
    }

    private void FixedUpdate()
    {
        if (_needPush == false || SpeedSetter.Instance.Setting == null)
            return;
        
        Vector3 moveVector = transform.forward * SpeedSetter.Instance.Setting.Speed;
        _rigidbody.velocity = new Vector3(moveVector.x, _rigidbody.velocity.y, moveVector.z);
    }
    
    public void Stop()
    {
        StopPushing();
        _rigidbody.velocity = Vector3.zero;
    }
    
    private void StopPushing()
    {
        _needPush = false;
    }
}
