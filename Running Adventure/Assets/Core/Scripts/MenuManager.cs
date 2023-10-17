using MyLibrary;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    SaveLoad _saveLoad = new SaveLoad();

    [SerializeField] private GameObject _quitPanel;
    private void Start()
    {
        _saveLoad.Check();
    }


    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Play()
    {
        SceneManager.LoadScene(_saveLoad.LoadInteger("LastLevel"));
       
    }

    public void QuitButton(string state)
    {
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
