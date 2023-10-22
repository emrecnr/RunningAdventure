using MyLibrary;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.SceneManagement;
using System.Reflection;

public class CustomizeManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _gemText;


    [SerializeField] private GameObject[] _panels;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject[] _generalPanels;
    [SerializeField] private Button[] _processButtons;
    [SerializeField] private TextMeshProUGUI _buyText;
    private int _activePanelIndex;
    [Header("-----CAPS-----")]
    [SerializeField] private Button[] _capButtons;
    [SerializeField] private GameObject[] _caps;
    [SerializeField] private TextMeshProUGUI _capText;
    [Header("-----STICKS-----")]
    [SerializeField] private GameObject[] _sticks;
    [SerializeField] private Button[] _stickButtons;
    [SerializeField] private TextMeshProUGUI _stickText;
    [Header("-----MATERIALS-----")]
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material[] _materials;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Button[] _materialButtons;
    [SerializeField] private TextMeshProUGUI _skinText;


    int _capIndex = -1;
    int _stickIndex = -1;
    int _skinIndex = -1;

    private SaveLoad _saveLoad = new SaveLoad();
    private DataController _dataController = new DataController();
    [Header("GENERAL DATA")]
    public List<ItemData> _itemData = new List<ItemData>();

    [SerializeField] Animator _equippedAnimator;
    [SerializeField] AudioSource[] _audios;

    private void Start()
    {
        // _saveLoad.SaveInteger("Gem", 6500);
        _gemText.text = _saveLoad.LoadInteger("Gem").ToString();


        _dataController.Load();
        _itemData = _dataController.GetList();
        CheckStatus(0, true);
        CheckStatus(1, true);
        CheckStatus(2, true);
        foreach (var item in _audios)
        {
            item.volume = _saveLoad.LoadFloat("MenuFX");
        }
    }

    public void CheckStatus(int section, bool status = false)
    {
        if (section == 0) // CAP
        {
            if (_saveLoad.LoadInteger("ActiveCap") == -1)
            {
                foreach (var item in _caps)
                {
                    item.SetActive(false);
                }

                _processButtons[0].interactable = false;
                _processButtons[1].interactable = false;
                _buyText.text = "BUY";
                if (!status)
                {
                    _capIndex = -1;
                    _capText.text = "None Cap";
                }


            }
            else
            {
                foreach (var item in _caps)
                {
                    item.SetActive(false);
                }
                _capIndex = _saveLoad.LoadInteger("ActiveCap");
                _caps[_capIndex].SetActive(true);

                _capText.text = _itemData[_capIndex].itemName;
                _buyText.text = "BUY";
                _processButtons[0].interactable = false;
                _processButtons[1].interactable = true;

            }
        }
        else if (section == 1) //STICK
        {

            if (_saveLoad.LoadInteger("ActiveStick") == -1)
            {
                foreach (var item in _sticks)
                {
                    item.SetActive(false);
                }
                _buyText.text = "BUY";

                _processButtons[0].interactable = false;
                _processButtons[1].interactable = false;
                if (!status)
                {
                    _stickIndex = -1;
                    _stickText.text = "None Stick";
                }

            }
            else
            {
                foreach (var item in _sticks)
                {
                    item.SetActive(false);
                }
                _stickIndex = _saveLoad.LoadInteger("ActiveStick");
                _sticks[_stickIndex].SetActive(true);

                _stickText.text = _itemData[_stickIndex + 3].itemName;
                _buyText.text = "BUY";
                _processButtons[0].interactable = false;
                _processButtons[1].interactable = true;
            }
        }
        else if (section == 2)// SKIN
        {
            if (_saveLoad.LoadInteger("ActiveSkin") == -1)
            {
                if (!status)
                {
                    _buyText.text = "BUY";
                    _skinIndex = -1;
                    _skinText.text = "None Skin";
                    _processButtons[0].interactable = false;
                    _processButtons[1].interactable = false;
                }
                else
                {
                    Material[] mats = _skinnedMeshRenderer.materials;
                    mats[0] = _defaultMaterial;
                    _skinnedMeshRenderer.materials = mats;
                    _buyText.text = "BUY";
                }

            }
            else
            {
                _skinIndex = _saveLoad.LoadInteger("ActiveSkin");
                Material[] mats = _skinnedMeshRenderer.materials;
                mats[0] = _materials[_skinIndex];
                _skinnedMeshRenderer.materials = mats;

                _skinText.text = _itemData[_skinIndex + 6].itemName;
                _buyText.text = "BUY";
                _processButtons[0].interactable = false;
                _processButtons[1].interactable = true;

            }
        }
    }
    public void Buy()
    {
        if (_activePanelIndex != -1)
        {
            _audios[1].Play();
            switch (_activePanelIndex)
            {
                case 0:
                    BuyResult(_capIndex);
                    break;
                case 1:
                    int indexStick = _stickIndex + 3;
                    BuyResult(indexStick);
                    break;
                case 2:
                    int indexSkin = _skinIndex + 6;
                    BuyResult(indexSkin);
                    break;
            }
        }

    }
    public void Equip()
    {
        _audios[2].Play();
        if (_activePanelIndex != -1)
        {
            switch (_activePanelIndex)
            {
                case 0:
                    SaveResult("ActiveCap", _capIndex);
                    break;
                case 1:
                    SaveResult("ActiveStick", _stickIndex);
                    break;

                case 2:
                    SaveResult("ActiveSkin", _skinIndex);
                    break;
            }
        }
    }

    public void CapButtons(string value)
    {
        _audios[0].Play();
        if (value == "Right")
        {
            if (_capIndex == -1)
            {
                _capIndex = 0;
                _caps[_capIndex].SetActive(true);
                _capText.text = _itemData[_capIndex].itemName;

                if (!_itemData[_capIndex].buyState)
                {
                    _buyText.text = _itemData[_capIndex].cost + "- BUY";
                    _processButtons[1].interactable = false;
                    if (_saveLoad.LoadInteger("Gem") < _itemData[_capIndex].cost)
                    {
                        _processButtons[0].interactable = false;
                    }
                    else
                    {
                        _processButtons[0].interactable = true;
                    }


                }
                else
                {
                    _buyText.text = "BUY";
                    _processButtons[0].interactable = false;
                    _processButtons[1].interactable = true;
                }
            }
            else
            {
                _caps[_capIndex].SetActive(false);
                _capIndex++;
                _caps[_capIndex].SetActive(true);
                _capText.text = _itemData[_capIndex].itemName;
                if (!_itemData[_capIndex].buyState)
                {
                    _buyText.text = _itemData[_capIndex].cost + "- BUY";

                    _processButtons[1].interactable = false;
                    if (_saveLoad.LoadInteger("Gem") < _itemData[_capIndex].cost)
                    {
                        _processButtons[0].interactable = false;
                    }
                    else
                    {
                        _processButtons[0].interactable = true;
                    }
                }
                else
                {
                    _buyText.text = "BUY";
                    _processButtons[0].interactable = false;
                    _processButtons[1].interactable = true;
                }

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
                    if (!_itemData[_capIndex].buyState)
                    {
                        _buyText.text = _itemData[_capIndex].cost + "- BUY";
                        _processButtons[1].interactable = false;
                        if (_saveLoad.LoadInteger("Gem") < _itemData[_capIndex].cost)
                        {
                            _processButtons[0].interactable = false;
                        }
                        else
                        {
                            _processButtons[0].interactable = true;
                        }
                    }
                    else
                    {
                        _buyText.text = "BUY";
                        _processButtons[0].interactable = false;
                        _processButtons[1].interactable = true;
                    }
                }
                else
                {
                    _capButtons[0].interactable = false;
                    _capText.text = "None Cap";
                    _buyText.text = "BUY";
                    _processButtons[0].interactable = false;
                }
            }
            else
            {
                _capButtons[0].interactable = false;
                _capText.text = "None Cap";
                _buyText.text = "BUY";
                _processButtons[0].interactable = false;
            }

            if (_capIndex != _caps.Length - 1)
            {
                _capButtons[1].interactable = true;
            }
        }
    }
    public void StickButtons(string value)
    {
        _audios[0].Play();
        if (value == "Right")
        {
            if (_stickIndex == -1)
            {
                _stickIndex = 0;
                _sticks[_stickIndex].SetActive(true);
                _stickText.text = _itemData[_stickIndex + 3].itemName;
                if (!_itemData[_stickIndex + 3].buyState)
                {
                    _buyText.text = _itemData[_stickIndex + 3].cost + "- BUY";
                    _processButtons[1].interactable = false;
                    if (_saveLoad.LoadInteger("Gem") < _itemData[_stickIndex + 3].cost)
                    {
                        _processButtons[0].interactable = false;
                    }
                    else
                    {
                        _processButtons[0].interactable = true;
                    }
                }
                else
                {
                    _buyText.text = "BUY";
                    _processButtons[0].interactable = false;
                    _processButtons[1].interactable = true;
                }
            }
            else
            {
                _sticks[_stickIndex].SetActive(false);
                _stickIndex++;
                _sticks[_stickIndex].SetActive(true);
                _stickText.text = _itemData[_stickIndex + 3].itemName;
                if (!_itemData[_stickIndex + 3].buyState)
                {
                    _buyText.text = _itemData[_stickIndex + 3].cost + "- BUY";
                    _processButtons[1].interactable = false;
                    if (_saveLoad.LoadInteger("Gem") < _itemData[_stickIndex + 3].cost)
                    {
                        _processButtons[0].interactable = false;
                    }
                    else
                    {
                        _processButtons[0].interactable = true;
                    }
                }
                else
                {
                    _buyText.text = "BUY";
                    _processButtons[0].interactable = false;
                    _processButtons[1].interactable = true;
                }
            }
            //--------------------//
            if (_stickIndex == _sticks.Length - 1)
            {
                _stickButtons[1].interactable = false;
            }
            else
            {
                _stickButtons[1].interactable = true;
            }

            if (_stickIndex != -1)
            {
                _stickButtons[0].interactable = true;
            }
        }
        else
        {
            if (_stickIndex != -1)
            {
                _sticks[_stickIndex].SetActive(false);
                _stickIndex--;
                if (_stickIndex != -1)
                {
                    _sticks[_stickIndex].SetActive(true);
                    _stickButtons[0].interactable = true;
                    _stickText.text = _itemData[_stickIndex + 3].itemName;
                    if (!_itemData[_stickIndex + 3].buyState)
                    {
                        _buyText.text = _itemData[_stickIndex + 3].cost + "- BUY";
                        _processButtons[1].interactable = false;
                        if (_saveLoad.LoadInteger("Gem") < _itemData[_stickIndex + 3].cost)
                        {
                            _processButtons[0].interactable = false;
                        }
                        else
                        {
                            _processButtons[0].interactable = true;
                        }
                    }
                    else
                    {
                        _buyText.text = "BUY";
                        _processButtons[0].interactable = false;
                        _processButtons[1].interactable = true;
                    }
                }
                else
                {
                    _stickButtons[0].interactable = false;
                    _stickText.text = "None Stick";
                    _buyText.text = "BUY";
                    _processButtons[0].interactable = false;
                }
            }
            else
            {
                _stickButtons[0].interactable = false;
                _stickText.text = "None Stick";
                _buyText.text = "BUY";
                _processButtons[0].interactable = false;
            }

            if (_stickIndex != _sticks.Length - 1)
            {
                _stickButtons[1].interactable = true;
            }
        }
    }
    public void SkinButtons(string value)
    {
        _audios[0].Play();
        if (value == "Right")
        {
            if (_skinIndex == -1)
            {
                _skinIndex = 0;
                Material[] mats = _skinnedMeshRenderer.materials;
                mats[0] = _materials[_skinIndex];
                _skinnedMeshRenderer.materials = mats;

                _skinText.text = _itemData[_skinIndex + 6].itemName;
                if (!_itemData[_skinIndex + 6].buyState)
                {
                    _buyText.text = _itemData[_skinIndex + 6].cost + "- BUY";
                    _processButtons[1].interactable = false;
                    if (_saveLoad.LoadInteger("Gem") < _itemData[_skinIndex + 6].cost)
                    {
                        _processButtons[0].interactable = false;
                    }
                    else
                    {
                        _processButtons[0].interactable = true;
                    }
                }
                else
                {
                    _buyText.text = "BUY";
                    _processButtons[0].interactable = false;
                    _processButtons[1].interactable = true;
                }
            }
            else
            {
                _skinIndex++;

                Material[] mats = _skinnedMeshRenderer.materials;
                mats[0] = _materials[_skinIndex];
                _skinnedMeshRenderer.materials = mats;


                _skinText.text = _itemData[_skinIndex + 6].itemName;
                if (!_itemData[_skinIndex + 6].buyState)
                {
                    _buyText.text = _itemData[_skinIndex + 6].cost + "- BUY";
                    _processButtons[1].interactable = false;
                    if (_saveLoad.LoadInteger("Gem") < _itemData[_skinIndex + 6].cost)
                    {
                        _processButtons[0].interactable = false;
                    }
                    else
                    {
                        _processButtons[0].interactable = true;
                    }
                }
                else
                {
                    _buyText.text = "BUY";
                    _processButtons[0].interactable = false;
                    _processButtons[1].interactable = true;
                }
            }
            //--------------------//
            if (_skinIndex == _materials.Length - 1)
            {
                _materialButtons[1].interactable = false;
            }
            else
            {
                _materialButtons[1].interactable = true;
            }

            if (_skinIndex != -1)
            {
                _materialButtons[0].interactable = true;
            }
        }
        else
        {
            if (_skinIndex != -1)
            {

                _skinIndex--;
                if (_skinIndex != -1)
                {
                    Material[] mats = _skinnedMeshRenderer.materials;
                    mats[0] = _materials[_skinIndex];
                    _skinnedMeshRenderer.materials = mats;

                    _materialButtons[0].interactable = true;
                    _skinText.text = _itemData[_skinIndex + 6].itemName;
                    if (!_itemData[_skinIndex + 6].buyState)
                    {
                        _buyText.text = _itemData[_skinIndex + 6].cost + "- BUY";
                        _processButtons[1].interactable = false;
                        if (_saveLoad.LoadInteger("Gem") < _itemData[_skinIndex + 6].cost)
                        {
                            _processButtons[0].interactable = false;
                        }
                        else
                        {
                            _processButtons[0].interactable = true;
                        }
                    }
                    else
                    {
                        _buyText.text = "BUY";
                        _processButtons[0].interactable = false;
                        _processButtons[1].interactable = true;
                    }
                }
                else
                {
                    Material[] mats = _skinnedMeshRenderer.materials;
                    mats[0] = _defaultMaterial;
                    _skinnedMeshRenderer.materials = mats;
                    _materialButtons[0].interactable = false;
                    _skinText.text = "None Skin";
                    _buyText.text = "BUY";
                    _processButtons[0].interactable = false;
                }
            }
            else
            {
                Material[] mats = _skinnedMeshRenderer.materials;
                mats[0] = _defaultMaterial;
                _skinnedMeshRenderer.materials = mats;

                _materialButtons[0].interactable = false;
                _skinText.text = "None Skin";
            }

            if (_skinIndex != _materials.Length - 1)
            {
                _materialButtons[1].interactable = true;
            }
        }
    }
    public void SetActivePanel(int index)
    {
        _audios[0].Play();
        CheckStatus(index);
        _generalPanels[0].SetActive(true);
        _activePanelIndex = index;
        _panels[index].SetActive(true);
        _generalPanels[1].SetActive(true);
        _canvas.SetActive(false);



    }
    public void Back()
    {
        _audios[0].Play();
        _generalPanels[0].SetActive(false);
        _canvas.SetActive(true);
        _generalPanels[1].SetActive(false);
        _panels[_activePanelIndex].SetActive(false);
        CheckStatus(_activePanelIndex, true);
        _activePanelIndex = -1;


    }
    public void BackToMenu()
    {
        _audios[0].Play();
        _dataController.Save(_itemData);
        SceneManager.LoadScene(0);

    }

    //-------
    private void BuyResult(int index)
    {
        _itemData[index].buyState = true;
        _saveLoad.SaveInteger("Gem", _saveLoad.LoadInteger("Gem") - _itemData[index].cost);
        _buyText.text = "BUY";
        _processButtons[0].interactable = false;
        _processButtons[1].interactable = true;
        _gemText.text = _saveLoad.LoadInteger("Gem").ToString();
    }
    private void SaveResult(string key, int index)
    {
        _saveLoad.SaveInteger(key, index);
        _processButtons[1].interactable = false;
        if (!_equippedAnimator.GetBool("isOk"))
        {
            _equippedAnimator.SetBool("isOk", true);
        }
    }

}
