using MyLibrary;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    SaveLoad _saveLoad = new SaveLoad();
    DataController _dataController = new DataController();
    [SerializeField] private GameObject _quitPanel;
    public List<ItemData> _itemData = new List<ItemData>();
    [SerializeField] private AudioSource _buttonMusic;
    private void Start()
    {
        _saveLoad.Check();
        _dataController.FirsTimeSave(_itemData);
        _buttonMusic.volume = _saveLoad.LoadFloat("MenuFX");
    }


    public void LoadScene(int sceneIndex)
    {
        _buttonMusic.Play();
        SceneManager.LoadScene(sceneIndex);
    }

    public void Play()
    {
        _buttonMusic.Play();
        SceneManager.LoadScene(_saveLoad.LoadInteger("LastLevel"));

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
