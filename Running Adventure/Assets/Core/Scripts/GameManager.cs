using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] public GameObject _spawnPoint;
    [SerializeField] public GameObject _targetPoint;

    [SerializeField] private List<GameObject> characters;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var character in characters)
            {
                if (!character.activeInHierarchy)
                {
                    character.transform.position = _spawnPoint.transform.position;
                    character.SetActive(true);
                    break; // ilk pasif objeyi bulunduðunda bitir.
                }

            }
        }
    }
}
