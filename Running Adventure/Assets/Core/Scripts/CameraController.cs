using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _target_offset;

    public bool isFinish;

    [SerializeField] private GameObject _cameraFinishObject;


    private void Start()
    {
        _target_offset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        if (isFinish)
        {
            transform.position = Vector3.Lerp(transform.position, _cameraFinishObject.transform.position + _target_offset, 0.015f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _target.position + _target_offset, 0.125f);
        }

    }
}
