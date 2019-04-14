using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPopper : Singleton<BalloonPopper>
{
    [SerializeField]
    private GameObject _balloonPopFeedbackPrefab;

    public void PopBalloon(GameObject balloonCore)
    {
        GameObject.Instantiate(_balloonPopFeedbackPrefab, balloonCore.transform.position, Quaternion.identity);
        balloonCore.GetComponent<FollowTarget>().Target.GetComponent<Rigidbody>().useGravity = true;
        Destroy(balloonCore);
    }

}
