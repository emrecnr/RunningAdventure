using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacter : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] public GameObject _targetPoint;

    NavMeshAgent _navMeshAgent;


    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();


    }
    private void LateUpdate()
    {
        _navMeshAgent.SetDestination(_targetPoint.transform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacles") || other.CompareTag("Saw") || other.CompareTag("fan"))
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
        else if (other.CompareTag("OtherCharacter"))
        {
            _gameManager.characters.Add(other.gameObject);
        }
    }
    private Vector3 GetVector()
    {
        return new Vector3(transform.position.x, 0.25f, transform.position.z);
    }
}
