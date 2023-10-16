using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject _attackTarget;
    NavMeshAgent _enemyNavMesh;
    bool _canAttack;

    private void Start()
    {
        _enemyNavMesh = GetComponent<NavMeshAgent>();   
    }

    private void Update()
    {
        if (_canAttack)
        {
            _enemyNavMesh.SetDestination(_attackTarget.transform.position);
        }
    }
    public void TriggerAnimation()
    {
        GetComponent<Animator>().SetBool("attack",true);
        _canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AICharacter"))
        {
            Vector3 offset = new Vector3(transform.position.x, 0.25f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestroyEffectCreate(offset,false,true);

            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
