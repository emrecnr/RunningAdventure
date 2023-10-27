using MyLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Icons;

public class MenuManager : MonoBehaviour
{
    SaveLoad _saveLoad = new SaveLoad();
    DataController _dataController = new DataController();
    [SerializeField] private GameObject _quitPanel;
    public List<ItemData> _defaultItemData = new List<ItemData>();
    public List<LanguageData> _defaultLanguageData = new List<LanguageData>();
    [SerializeField] private AudioSource _buttonMusic;

    public List<LanguageData> languageDatas = new List<LanguageData>();
    private List<LanguageData> _readedData = new List<LanguageData>();
    public TextMeshProUGUI[] _languageTexts;

    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private Slider _loadingSlider;
    private void Start()
    {
        _saveLoad.Check();
        _dataController.FirsTimeSave(_defaultItemData, _defaultLanguageData);
        _buttonMusic.volume = _saveLoad.LoadFloat("MenuFX");

        //_saveLoad.SaveString("Language", "TR");

        _dataController.LanguageLoad();
        _readedData = _dataController.LanguageTransaction();
        languageDatas.Add(_readedData[0]);
        LanguagePreference();
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



    public void LoadScene(int sceneIndex)
    {
        _buttonMusic.Play();
        SceneManager.LoadScene(sceneIndex);
    }

    public void Play()
    {
        _buttonMusic.Play();
        
        StartCoroutine(LoadAsync(_saveLoad.LoadInteger("LastLevel")));
    }
    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        _loadingPanel.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            _loadingSlider.value = progress;
            yield return null;
        }
        

    }
    public void QuitButton(string state)
    {
        _buttonMusic.Play();
        if (state == "Yes")
        {
            Application.Quit();
        }
        else if (state == "Quit")
        {
            _quitPanel.SetActive(true);
        }
        else
        {
            _quitPanel.SetActive(false);
        }
    }
}
