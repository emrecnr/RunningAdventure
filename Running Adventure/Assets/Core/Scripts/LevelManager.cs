using MyLibrary;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    DataController _dataController = new DataController();
    SaveLoad _saveLoad = new SaveLoad();

    [SerializeField] Button[] _levelButtons;
    [SerializeField] private Sprite _lockSprite;
    [SerializeField] private AudioSource _audio;
    int level;
    [Header("---LANGUAGE DATA---")]
    public List<LanguageData> languageDatas = new List<LanguageData>();
    private List<LanguageData> _readedData = new List<LanguageData>();
    public TextMeshProUGUI[] _languageTexts;

    [Header("---LOADING DATA---")]
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private Slider _loadingSlider;
    private void Start()
    {
        _dataController.LanguageLoad();
        _readedData = _dataController.LanguageTransaction();
        languageDatas.Add(_readedData[2]);
        LanguagePreference();
        _audio.volume = _saveLoad.LoadFloat("MenuFX");

        int currentLevel = _saveLoad.LoadInteger("LastLevel") - 4; // ilk level sahne indexi 5 oldugu icin
        int index = 1;
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            if (index<= currentLevel)
            {
                _levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = index.ToString();
                int sceneIndex = index + 4;
                _levelButtons[i].onClick.AddListener(delegate { LoadScene(sceneIndex); });
            }
            else
            {
                _levelButtons[i].GetComponent<Image>().sprite = _lockSprite;
                _levelButtons[i].enabled = false; // soluklastirma olmamasi icin
            }
            index++;
        }

    }
    private void LanguagePreference()
    {
        if (_saveLoad.LoadString("Language") == "TR")
        {
            for (int i = 0; i < _languageTexts.Length; i++)
            {
                _languageTexts[i].text = languageDatas[0]._languageTR[0].Text;
            }
        }
        else
        {
            for (int i = 0; i < _languageTexts.Length; i++)
            {
                _languageTexts[i].text = languageDatas[0]._languageENG[0].Text;
            }
        }
    }
    public void LoadScene(int index)
    {
        _audio.Play();
       
        StartCoroutine(LoadAsync(index));
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
    public void Back()
    {
        _audio.Play();
        // MENU
        SceneManager.LoadScene(0);
    }
}
