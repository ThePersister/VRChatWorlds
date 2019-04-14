using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private float _followSpeed = 10;

    private Transform _transform;

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
        _transform.position = Vector3.Lerp(_transform.position, _target.position + _offset, Time.deltaTime * _followSpeed);
    }
}
