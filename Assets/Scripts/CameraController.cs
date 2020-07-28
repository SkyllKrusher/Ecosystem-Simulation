using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Vector3 restPos;
    public Vector3 restRot;
    public Vector3 offsetPos;
    public Vector3 offsetRot;
    public bool toFollow;
    public Transform followTarget;
    public Button followBtn;

    private void Awake()
    {
        followBtn.onClick.AddListener(OnClickFollow);
    }
    private void Start()
    {
        toFollow = false;
        ResetCam();
    }

    private void FixedUpdate()
    {
        CameraFollow();
    }

    private void SetFollowTarget(Transform target)
    {
        followTarget = target;
    }

    private void StopFollowing()
    {
        toFollow = false;
        ResetCam();
    }

    private void ResetCam()
    {
        Camera.main.transform.position = restPos;
        Camera.main.transform.rotation = Quaternion.Euler(restRot);
    }

    private void StartFollowing(Transform target)
    {
        SetFollowTarget(target);
        toFollow = true;
    }

    private void OnClickFollow() //testing. TODO: update later
    {
        if (!toFollow)
        {
            StartFollowing(((Animal)FindObjectOfType(typeof(Animal))).gameObject.transform);
        }
        else
        {
            StopFollowing();
        }
    }

    private Vector3 CalculateRotatedPositionOffset()
    {
        Transform targetTransform = followTarget.transform;
        Vector3 newCamPos;

        newCamPos = (targetTransform.right * offsetPos.x + targetTransform.up * offsetPos.y + targetTransform.forward * offsetPos.z);
        return newCamPos;
    }

    public void CameraFollow()
    {
        if (toFollow)
        {
            // Camera.main.transform.position = followTarget.position + offsetPos;
            Camera.main.transform.position = CalculateRotatedPositionOffset() + followTarget.transform.position;
            Camera.main.transform.forward = followTarget.forward + offsetRot;
        }
    }
}
