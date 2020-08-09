using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Transform followTarget;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        // transform.LookAt(cam.transform.position);
        transform.position = cam.WorldToScreenPoint(followTarget.position + Offset);
    }
}
