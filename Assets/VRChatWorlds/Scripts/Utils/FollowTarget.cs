using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private float _followSpeed = 10;

    private Transform _transform;

    public Transform Target
    {
        get
        {
            return _target;
        }
    }

	private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        Follow();   
    }

    private void Follow()
    {
        if (_target != null)
        {
            _transform.position = Vector3.Lerp(_transform.position, _target.position + _offset, Time.deltaTime * _followSpeed);
        }
        else
        {
            this.enabled = false;
        }
    }
}
