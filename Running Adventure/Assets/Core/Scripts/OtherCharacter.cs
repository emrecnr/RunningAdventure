using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OtherCharacter : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Animator _animator;
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private Material _newMaterial;

    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] private GameObject _targetPoint;

    bool isTriggered;

    private void LateUpdate()
    {
        if (isTriggered)
        {
            _navMeshAgent.SetDestination(_targetPoint.transform.position);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("AICharacter"))
        {
            if (gameObject.CompareTag("OtherCharacter"))
            {
                Change();
                isTriggered = true;
            }
        }
        else if (other.CompareTag("Obstacles") || other.CompareTag("Saw") || other.CompareTag("fan"))
        {
            _gameManager.DestroyEffectCreate(GetVector());
            transform.position = Vector3.zero;
            gameObject.SetActive(false);

        }
        else if (other.CompareTag("Hammer"))
        {
            _gameManager.DestroyEffectCreate(GetVector(), true);
            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Enemy"))
        {

            _gameManager.DestroyEffectCreate(GetVector(), false, false);
            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

    private void Change()
    {
        Material[] mats = _renderer.materials;
        mats[0] = _newMaterial;
        _renderer.materials = mats;
        _animator.SetBool("attack", true);
        gameObject.tag = "AICharacter";
        GameManager._currentCharacterCount++;
    }
    private Vector3 GetVector()
    {
        return new Vector3(transform.position.x, 0.25f, transform.position.z);
    }
}


