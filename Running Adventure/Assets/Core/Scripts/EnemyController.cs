using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] private GameObject _attackTarget;
    [SerializeField] NavMeshAgent _enemyNavMesh;
    [SerializeField] Animator _animator;
    bool _canAttack;

  

    private void LateUpdate()
    {
        if (_canAttack)
        {
            _enemyNavMesh.SetDestination(_attackTarget.transform.position);
        }
    }
    public void TriggerAnimation()
    {
        _animator.SetBool("attack",true);
        _canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AICharacter"))
        {
            Vector3 offset = new Vector3(transform.position.x, 0.25f, transform.position.z);
            _gameManager.DestroyEffectCreate(offset,false,true);

            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
