using MyLibrary;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    SaveLoad _saveLoad = new SaveLoad();

    DataController _dataController = new DataController();

    [Header("---AUDIO---")]
    [SerializeField] private AudioSource _buttonMusic;
    [SerializeField] private Slider _menuMusic;
    [SerializeField] private Slider _menuFx;
    [SerializeField] private Slider _gameMusic;

    [Header("---LANGUAGE DATA---")]
    public List<LanguageData> languageDatas = new List<LanguageData>();
    private List<LanguageData> _readedData = new List<LanguageData>();
    public TextMeshProUGUI[] _languageTexts;

    [Header("Language Chose")]
    public TextMeshProUGUI _languageText;
    public Button[] _languageButtons;
#pragma warning disable IDE0052 // Okunmamýþ özel üyeleri kaldýr
    private int _activeIndex = 0;
#pragma warning restore IDE0052 // Okunmamýþ özel üyeleri kaldýr

    private void Start()
    {
        _dataController.LanguageLoad();
        _readedData = _dataController.LanguageTransaction();
        languageDatas.Add(_readedData[4]);
        LanguagePreference();
        CheckLanguageState();

        _buttonMusic.volume = _saveLoad.LoadFloat("MenuFX");


        _menuMusic.value = _saveLoad.LoadFloat("MenuMusic");
        _menuFx.value = _saveLoad.LoadFloat("MenuFX");
        _gameMusic.value = _saveLoad.LoadFloat("GameMusic");
    }
    private void LanguagePreference()
    {
        if (_saveLoad.LoadString("Language") == "TR")
        {
            for (int i = 0; i < _languageTexts.Length; i++)
            {
                _languageTexts[i].text = languageDatas[0]._languageTR[i].Text;
            }
        }
        else
        {
            for (int i = 0; i < _languageTexts.Length; i++)
            {
                _languageTexts[i].text = languageDatas[0]._languageENG[i].Text;
            }
        }
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
    private void CheckLanguageState()
    {
        if (_saveLoad.LoadString("Language") == "TR")
        {
            _activeIndex = 0;
            _languageText.text = "TÜRKÇE";
            _languageButtons[0].gameObject.SetActive(false);
        }
        else
        {
            _activeIndex = 1;
            _languageText.text = "ENGLISH";
            _languageButtons[1].gameObject.SetActive(false);
            
        }
    }
    public void ChangeLanguange(string value)
    {
        switch (value)
        {
            case "Right":
                _activeIndex = 1;
                _languageText.text = "ENGLISH";
                _languageButtons[1].gameObject.SetActive(false);
                _languageButtons[0].gameObject.SetActive(true);
                _saveLoad.SaveString("Language", "ENG");
                LanguagePreference();
                break;

            case "Left":
                _activeIndex = 0;
                _languageText.text = "TÜRKÇE";
                _languageButtons[0].gameObject.SetActive(false);
                _languageButtons[1].gameObject.SetActive(true);
                _saveLoad.SaveString("Language", "TR");
                LanguagePreference();
                break;
        }
        _buttonMusic.Play();
    }
}
