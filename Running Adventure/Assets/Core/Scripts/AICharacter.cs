using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacter : MonoBehaviour
{
    private GameObject _target;
    NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindWithTag("GameManager").GetComponent<GameManager>()._targetPoint;
    }
    private void LateUpdate()
    {
        _navMeshAgent.SetDestination(_target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacles"))
        {
            GameManager._currentCharacterCount--;
            gameObject.SetActive(false);

        }
    }
}
