using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{
    [SerializeField] private Vector3 Offset;
    private Transform followTarget;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        // transform.LookAt(cam.transform.position);
        transform.position = cam.WorldToScreenPoint(followTarget.position + Offset);
    }

    public void Init(Transform target)
    {
        followTarget = target;
    }
}
