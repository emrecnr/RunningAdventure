using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    private static GameObject instance;

    AudioSource _menuAudio;
    private void Start()
    {
        _menuAudio = GetComponent<AudioSource>();
        _menuAudio.volume = PlayerPrefs.GetFloat("MenuMusic");
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        _menuAudio.volume = PlayerPrefs.GetFloat("MenuMusic");
    }
}
