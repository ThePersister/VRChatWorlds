using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NegativeGravity : MonoBehaviour {

    [SerializeField]
    private float _forceMultiplier = 1.0f;

    private Rigidbody _rigidbody;

	private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
    {
        _rigidbody.AddForce(Vector3.up * Time.deltaTime * _forceMultiplier, ForceMode.Force);
	}
}
