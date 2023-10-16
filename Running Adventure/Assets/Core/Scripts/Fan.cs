using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float delay;

    [SerializeField] private BoxCollider _stormCollider;


    public void AnimationStart(string state)
    {
        if (state == "true")
        {
            _animator.SetBool("start", true);
            _stormCollider.enabled = true;
        }
        else
        {
            _animator.SetBool("start", false);
            StartCoroutine(TriggerAnimation());
            _stormCollider.enabled = false;
        }



        

    }

    IEnumerator TriggerAnimation()
    {
        yield return new WaitForSeconds(delay);
        AnimationStart("true");
    }
}
