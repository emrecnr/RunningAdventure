using MyLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _buttonMusic;
    [SerializeField] private Slider _menuMusic;
    [SerializeField] private Slider _menuFx;
    [SerializeField] private Slider _gameMusic;

    SaveLoad _saveLoad = new SaveLoad();

    private void Start()
    {
        _buttonMusic.volume = _saveLoad.LoadFloat("MenuFX");


        _menuMusic.value = _saveLoad.LoadFloat("MenuMusic");
        _menuFx.value = _saveLoad.LoadFloat("MenuFX");
        _gameMusic.value = _saveLoad.LoadFloat("GameMusic");
    }











    public void VolumeSetting(string which)
    {
        switch (which)
        {
            case "MenuMusic":
                _saveLoad.SaveFloat("MenuMusic", _menuMusic.value);
                break;
            case "MenuFX":
                _saveLoad.SaveFloat("MenuFX", _menuFx.value);
                break;
            case "GameMusic":
                _saveLoad.SaveFloat("GameMusic", _gameMusic.value);
                break;


        }
    }



    public void BackToMenu()
    {
        _buttonMusic.Play();
        SceneManager.LoadScene(0);
    }
    public void ChangeLanguange()
    {
        _buttonMusic.Play();
    }
}
