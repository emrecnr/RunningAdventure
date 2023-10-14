using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] public GameObject _spawnPoint;
    [SerializeField] public GameObject _targetPoint;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_characterPrefab,_spawnPoint.transform.position,Quaternion.identity);
        }
    }
}
