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
    SaveLoad _saveLoad = new SaveLoad();
    [SerializeField] Button[] _levelButtons;
    [SerializeField] private Sprite _lockSprite;
    [SerializeField] private AudioSource _audio;
    int level;
    private void Start()
    {
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
    public void LoadScene(int index)
    {
        _audio.Play();
        SceneManager.LoadScene(index);
    }

    public void Back()
    {
        _audio.Play();
        // MENU
        SceneManager.LoadScene(0);
    }
}
