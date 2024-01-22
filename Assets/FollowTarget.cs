 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform targetToFollow;

    [Space(20)]

    [SerializeField] bool lockOnXPos = false;
    [SerializeField] bool lockOnYPos = false;
    [SerializeField] bool lockOnZPos = false;

    [Space(20)]

    [SerializeField] bool lockOnXRot = false;
    [SerializeField] bool lockOnYRot = false;
    [SerializeField] bool lockOnZRot = false;

    Vector3 targetPosition;
    Vector3 targetEulerAngles;
    private void Update()
        {
            targetPosition = transform.position;
            targetEulerAngles = transform.eulerAngles;

            if(lockOnXPos) targetPosition.x = targetToFollow.position.x;
            if(lockOnYPos) targetPosition.y = targetToFollow.position.y;
            if(lockOnZPos) targetPosition.z = targetToFollow.position.z;


            if(lockOnXRot) targetEulerAngles.x = targetToFollow.eulerAngles.x;
            if(lockOnYRot) targetEulerAngles.y = targetToFollow.eulerAngles.y;
            if(lockOnZRot) targetEulerAngles.z = targetToFollow.eulerAngles.z;

            transform.position = targetPosition;
            transform.eulerAngles = targetEulerAngles;
        }
}
