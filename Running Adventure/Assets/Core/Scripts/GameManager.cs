using MyLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    Maths _math = new Maths();
    SaveLoad _saveLoad = new SaveLoad();

    public static int _currentCharacterCount;

    [SerializeField] public List<GameObject> characters;
    [SerializeField] private List<GameObject> spawnEffects;
    [SerializeField] private List<GameObject> destroyEffects;
    [SerializeField] private List<GameObject> stainEffects;

    [Header("LEVEL DATA")]
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private int howManyEnemy;
    [SerializeField] private GameObject _mainCharacter;
    public bool isGameOver = false;
    public bool isFinish;


    private void Start()
    {
        CreateEnemy();

        _saveLoad.SaveString("Name", "Platanus");

        _saveLoad.SaveFloat("Score", 3f);
    }
    private void Update()
    {

    }

    public void CreateEnemy()
    {
        for (int i = 0; i < howManyEnemy; i++)
        {
            enemies[i].SetActive(true);
        }
    }
    public void TriggerEnemy()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                enemy.GetComponent<EnemyController>().TriggerAnimation();
            }

        }
        isFinish = true;
        BattleState();
    }
    private void BattleState()
    {
        if (isFinish)
        {
            if (_currentCharacterCount == 1 || howManyEnemy == 0)
            {
                isGameOver = true;
                foreach (var enemy in enemies)
                {
                    if (enemy.activeInHierarchy)
                    {
                        enemy.GetComponent<Animator>().SetBool("attack", false);

                    }

                }
                foreach (var character in characters)
                {
                    if (character.activeInHierarchy)
                    {
                        character.GetComponent<Animator>().SetBool("attack", false);

                    }

                }
                _mainCharacter.GetComponent<Animator>().SetBool("attack", false);
                if (_currentCharacterCount < howManyEnemy || _currentCharacterCount == howManyEnemy)
                {
                    Debug.Log("Game Over");
                }
                else
                {
                    if (_currentCharacterCount > 5)
                    {
                        _saveLoad.SaveInteger("Score", _saveLoad.LoadInteger("Score") + 600);
                        _saveLoad.SaveInteger("LastLevel", _saveLoad.LoadInteger("LastLevel" + 1));
                    }
                    else
                    {
                        _saveLoad.SaveInteger("Score", _saveLoad.LoadInteger("Score") + 200);
                        _saveLoad.SaveInteger("LastLevel", _saveLoad.LoadInteger("LastLevel" + 1));
                    }
                    Debug.Log("You Win");

                }
            }
        }

    }
    public void AICharacterControl(string type, int value, Transform _position)
    {
        switch (type)
        {
            case "Multiplication":
                _math.Multiplication(value, characters, _position, spawnEffects);
                break;


            case "Addition":
                _math.Addition(value, characters, _position, spawnEffects);
                break;

            case "Substraction":

                _math.Substraction(value, characters, destroyEffects);

                break;

            case "Division":

                _math.Division(value, characters, destroyEffects);

                break;
        }
    }

    public void DestroyEffectCreate(Vector3 _position, bool hammer = false, bool state = false)
    {
        foreach (var effect in destroyEffects)
        {
            if (!effect.activeInHierarchy)
            {
                effect.SetActive(true);
                effect.transform.position = _position;
                effect.GetComponent<ParticleSystem>().Play();
                effect.GetComponent<AudioSource>().Play();
                if (!state)
                {
                    _currentCharacterCount--;
                }
                else
                {
                    howManyEnemy--;
                }

                break;
            }
        }

        if (hammer)
        {
            foreach (var effect in stainEffects)
            {
                Vector3 offset = new Vector3(_position.x, 0.005f, _position.z);
                if (!effect.activeInHierarchy)
                {
                    effect.SetActive(true);
                    effect.transform.position = offset;
                    break;


                }
            }
        }
        if (!isGameOver)
        {
            BattleState();
        }
    }

}
