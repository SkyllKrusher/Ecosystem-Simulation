using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Vector3 restPos;
    public Vector3 Offset;
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
        restPos = new Vector3(0, 63 - 137);
        ResetCamPosition();
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
        ResetCamPosition();
    }

    private void ResetCamPosition()
    {
        Camera.main.transform.position = restPos;
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

    public void CameraFollow()
    {
        if (toFollow)
        {
            Camera.main.transform.position = followTarget.position + Offset;
        }
    }
}
