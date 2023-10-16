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
        if (other.CompareTag("Obstacles")||other.CompareTag("Saw")||other.CompareTag("fan"))
        {
            Vector3 offset = new Vector3(transform.position.x,0.25f,transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestroyEffectCreate(offset);
            transform.position = Vector3.zero;
            gameObject.SetActive(false);

        }
        if(other.CompareTag("Hammer"))
        {
            Vector3 offset = new Vector3(transform.position.x, 0.25f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestroyEffectCreate(offset,true);
           
            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Enemy"))
        {
            Vector3 offset = new Vector3(transform.position.x, 0.25f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestroyEffectCreate(offset,false,false);

            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
