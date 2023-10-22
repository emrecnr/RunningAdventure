using MyLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI ;


public class GameManager : MonoBehaviour
{

    Maths _math = new Maths();
    SaveLoad _saveLoad = new SaveLoad();

    public static int _currentCharacterCount;

    [SerializeField] public List<GameObject> characters;
    [Header("EFFECTS")]
    [SerializeField] private List<GameObject> spawnEffects;
    [SerializeField] private List<GameObject> destroyEffects;
    [SerializeField] private List<GameObject> stainEffects;

    [Header("LEVEL DATA")]
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private int howManyEnemy;
    [SerializeField] private GameObject _mainCharacter;
    public bool isGameOver = false;
    public bool isFinish;
    [Header("-----CAPS-----")]
    [SerializeField] private GameObject[] _caps;

    [Header("-----STICKS-----")]
    [SerializeField] private GameObject[] _sticks;

    [Header("-----MATERIALS-----")]
    [SerializeField] private Material[] _materials;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;


    Scene _scene;
    [Header("-----GENERAL DATA-----")]
    [SerializeField] private AudioSource[] _audios;
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private Slider _gameMusicSlider;
    
    private void Awake()
    {
        _audios[0].volume = _saveLoad.LoadFloat("GameMusic");
        _gameMusicSlider.value = _saveLoad.LoadFloat("GameMusic");
        _audios[1].volume = _saveLoad.LoadFloat("MenuFX");
        Destroy(GameObject.FindWithTag("MenuMusic"));
        CheckItem();
    } 
    private void Start()
    {
        CreateEnemy();
        _scene = SceneManager.GetActiveScene();

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
                        if (_scene.buildIndex == _saveLoad.LoadInteger("LastLevel"))
                        {
                            _saveLoad.SaveInteger("Gem", _saveLoad.LoadInteger("Gem") + 600);
                            _saveLoad.SaveInteger("LastLevel", _saveLoad.LoadInteger("LastLevel" + 1));
                        }

                    }
                    else
                    {
                        if (_scene.buildIndex == _saveLoad.LoadInteger("LastLevel"))
                        {
                            _saveLoad.SaveInteger("Gem", _saveLoad.LoadInteger("Gem") + 200);
                            _saveLoad.SaveInteger("LastLevel", _saveLoad.LoadInteger("LastLevel" + 1));
                        }
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

    public void CheckItem()
    {
        if (_saveLoad.LoadInteger("ActiveCap") != -1)
        {
            _caps[_saveLoad.LoadInteger("ActiveCap")].SetActive(true);
        }
        if (_saveLoad.LoadInteger("ActiveStick") != -1)
        {
            _sticks[_saveLoad.LoadInteger("ActiveStick")].SetActive(true);
        }
        if (_saveLoad.LoadInteger("ActiveSkin") != -1)
        {
            Material[] mats = _skinnedMeshRenderer.materials;
            mats[0] = _materials[_saveLoad.LoadInteger("ActiveSkin")];
            _skinnedMeshRenderer.materials = mats;
        }
        else
        {
            Material[] mats = _skinnedMeshRenderer.materials;
            mats[0] = _defaultMaterial;
            _skinnedMeshRenderer.materials = mats;
        }



    }

    public void QuitButton(string state)
    {
        _audios[1].Play();
        Time.timeScale = 0;
        if (state == "Pause")
        {
            _panels[0].SetActive(true);
        }
        else if (state == "Home")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);            
        }
        else if (state == "Resume")
        {
            _panels[0].SetActive(false);
            Time.timeScale = 1;
        }
        else if (state == "Replay")
        {
            SceneManager.LoadScene(_scene.buildIndex);
            Time.timeScale = 1;
        }
    }

    public void Settings(string state)
    {
        if (state == "Settings")
        {
            Time.timeScale = 0;
            _panels[1].SetActive(true);            
        }
        else
        {
            Time.timeScale = 1;
            _panels[1].SetActive(false);
        }
    }
    public void VolumeSet()
    {
        _saveLoad.SaveFloat("GameMusic", _gameMusicSlider.value);
        _audios[0].volume = _gameMusicSlider.value;
        
    }
        
}

