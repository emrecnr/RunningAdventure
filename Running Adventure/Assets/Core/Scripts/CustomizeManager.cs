using MyLibrary;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class CustomizeManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _gemText;
    [SerializeField] private TextMeshProUGUI _capText;
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject[] _buttons;
    private int _activePanelIndex;
    [Header("CAPS")]
    [SerializeField] private Button[] _capButtons;
    [SerializeField] private GameObject[] _caps;
    [Header("STICKS")]
    [SerializeField] private GameObject[] _sticks;
    [Header("MATERIALS")]
    [SerializeField] private Material[] _materials;


    int _capIndex = -1;

    private SaveLoad _saveLoad = new SaveLoad();
    private DataController _dataController = new DataController();

    public List<ItemData> _itemData = new List<ItemData>();

    private void Start()
    {
        _saveLoad.SaveInteger("ActiveCap", -1);
        if (_saveLoad.LoadInteger("ActiveCap") == -1)
        {
            foreach (var item in _caps)
            {
                item.SetActive(false);
            }
            _capIndex = -1;
            _capText.text = "None Cap";
        }
        else
        {
            _capIndex = _saveLoad.LoadInteger("ActiveCap");
            _caps[_capIndex].SetActive(true);
        }
        //_dataController.Save(_itemData);
        _dataController.Load();
        _itemData = _dataController.GetList();
    }
    public void CapButtons(string value)
    {
        if (value == "Right")
        {
            if (_capIndex == -1)
            {
                _capIndex = 0;
                _caps[_capIndex].SetActive(true);
                _capText.text = _itemData[_capIndex].itemName;
            }
            else
            {
                _caps[_capIndex].SetActive(false);
                _capIndex++;
                _caps[_capIndex].SetActive(true);
                _capText.text = _itemData[_capIndex].itemName;
            }
            //--------------------//
            if (_capIndex == _caps.Length - 1)
            {
                _capButtons[1].interactable = false;
            }
            else
            {
                _capButtons[1].interactable = true;
            }

            if (_capIndex != -1)
            {
                _capButtons[0].interactable = true;
            }
        }
        else
        {
            if (_capIndex != -1)
            {
                _caps[_capIndex].SetActive(false);
                _capIndex--;
                if (_capIndex != -1)
                {
                    _caps[_capIndex].SetActive(true);
                    _capButtons[0].interactable = true;
                    _capText.text = _itemData[_capIndex].itemName;
                }
                else
                {
                    _capButtons[0].interactable = false;
                    _capText.text = "None Cap";
                }
            }
            else
            {
                _capButtons[0].interactable = false;
                _capText.text = "None Cap";
            }

            if (_capIndex != _caps.Length - 1)
            {
                _capButtons[1].interactable = true;
            }
        }
    }
    
    public void  SetActivePanel(int index)
    {
        _activePanelIndex = index;
        _panels[index].SetActive(true);
        _canvas.SetActive(false);
        _buttons[0].SetActive(true);
    }
    public void Back()
    {
        _buttons[0].SetActive(false);
        _canvas.SetActive(true);
        _panels[_activePanelIndex].SetActive(false);

    }

}
