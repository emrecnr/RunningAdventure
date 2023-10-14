using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _target_offset;


    private void Start()
    {
        _target_offset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,_target.position + _target_offset,0.125f);
    }
}
