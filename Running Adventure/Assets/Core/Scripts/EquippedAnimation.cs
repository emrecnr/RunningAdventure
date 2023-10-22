using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    public void SetActiveFalse()
    {
        _animator.SetBool("isOk", false);
    }
}
