using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tack : MonoBehaviour
{
    [SerializeField]
    private LayerMask _onlyBalloonCore;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitBalloon = collision.rigidbody.gameObject;
        if (hitBalloon.layer == Mathf.Log(_onlyBalloonCore.value, 2))
        {
            BalloonPopper.Instance.PopBalloon(hitBalloon);
        }
    }
}
