using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float _moveSpeed = .5f;
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetAxis("Mouse X")<0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z),.3f);
            }
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
            }
        }
    }



}
