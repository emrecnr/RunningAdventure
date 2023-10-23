using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyLibrary
{
    public class Maths
    {
        public void Multiplication(int value, List<GameObject> characters, Transform _position, List<GameObject> spawnParticles)
        {
            int loopTime = (GameManager._currentCharacterCount * value) - GameManager._currentCharacterCount;
            int num = 0;

            foreach (var character in characters)
            {

                if (num < loopTime)
                {
                    if (!character.activeInHierarchy)
                    {
                        foreach (var spawn in spawnParticles)
                        {
                            if (!spawn.activeInHierarchy)
                            {

                                spawn.SetActive(true);
                                spawn.transform.position = _position.position;
                                spawn.GetComponent<ParticleSystem>().Play();
                                spawn.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        character.transform.position = _position.position;
                        character.SetActive(true);
                        num++;

                    }
                }
                else
                {
                    num = 0;
                    break; // ilk pasif objeyi bulunduðunda bitir.
                }


            }
            GameManager._currentCharacterCount *= value;

        }
        public void Addition(int value, List<GameObject> characters, Transform _position, List<GameObject> spawnParticles)
        {
            int num2 = 0;
            foreach (var character in characters)
            {

                if (num2 < value)
                {
                    if (!character.activeInHierarchy)
                    {
                        foreach (var spawn in spawnParticles)
                        {
                            if (!spawn.activeInHierarchy)
                            {

                                spawn.SetActive(true);
                                spawn.transform.position = _position.position;
                                spawn.GetComponent<ParticleSystem>().Play();
                                spawn.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        character.transform.position = _position.position;
                        character.SetActive(true);
                        num2++;

                    }
                }
                else
                {
                    num2 = 0;
                    break; // ilk pasif objeyi bulunduðunda bitir.
                }


            }
            GameManager._currentCharacterCount += value;

        }
        public void Substraction(int value, List<GameObject> characters, List<GameObject> destroyParticles)
        {
            if (GameManager._currentCharacterCount < value)
            {
                foreach (var character in characters)
                {
                    foreach (var destroy in destroyParticles)
                    {
                        if (!destroy.activeInHierarchy)
                        {
                            Vector3 offset = new Vector3(character.transform.position.x, 0.25f, character.transform.position.z);
                            destroy.SetActive(true);
                            destroy.transform.position = offset;
                            destroy.GetComponent<ParticleSystem>().Play();
                            destroy.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    character.transform.position = Vector3.zero;
                    character.SetActive(false);
                }
                GameManager._currentCharacterCount = 1;
            }
            else
            {
                int num3 = 0;
                foreach (var character in characters)
                {

                    if (num3 != value)
                    {

                        if (character.activeInHierarchy)
                        {
                            foreach (var destroy in destroyParticles)
                            {
                                if (!destroy.activeInHierarchy)
                                {
                                    Vector3 offset = new Vector3(character.transform.position.x, 0.25f, character.transform.position.z);
                                    destroy.SetActive(true);
                                    destroy.transform.position = offset;
                                    destroy.GetComponent<ParticleSystem>().Play();
                                    destroy.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            character.transform.position = Vector3.zero;
                            character.SetActive(false);
                            num3++;

                        }
                    }
                    else
                    {
                        num3 = 0;
                        break; // ilk pasif objeyi bulunduðunda bitir.
                    }


                }
                GameManager._currentCharacterCount -= value;
            }

        }
        public void Division(int value, List<GameObject> characters, List<GameObject> destroyParticles)
        {
            if (GameManager._currentCharacterCount <= value)
            {
                foreach (var character in characters)
                {
                    foreach (var destroy in destroyParticles)
                    {
                        if (!destroy.activeInHierarchy)
                        {
                            Vector3 offset = new Vector3(character.transform.position.x, 0.25f, character.transform.position.z);
                            destroy.SetActive(true);
                            destroy.transform.position = offset;
                            destroy.GetComponent<ParticleSystem>().Play();
                            destroy.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    character.transform.position = Vector3.zero;
                    character.SetActive(false);
                }
                GameManager._currentCharacterCount = 1;
            }
            else
            {
                int sum = GameManager._currentCharacterCount / value;
                int num3 = 0;
                foreach (var character in characters)
                {

                    if (num3 != sum)
                    {
                        if (character.activeInHierarchy)
                        {
                            foreach (var destroy in destroyParticles)
                            {
                                if (!destroy.activeInHierarchy)
                                {
                                    Vector3 offset = new Vector3(character.transform.position.x, 0.25f, character.transform.position.z);
                                    destroy.SetActive(true);
                                    destroy.transform.position = offset;
                                    destroy.GetComponent<ParticleSystem>().Play();
                                    destroy.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            character.transform.position = Vector3.zero;
                            character.SetActive(false);
                            num3++;

                        }
                    }
                    else
                    {
                        num3 = 0;
                        break; // ilk pasif objeyi bulunduðunda bitir.
                    }


                }
                if (GameManager._currentCharacterCount % value == 0)
                {
                    GameManager._currentCharacterCount /= value;
                }
                else if (GameManager._currentCharacterCount % value == 1)
                {
                    GameManager._currentCharacterCount /= value;
                    GameManager._currentCharacterCount++;
                }
                else if (GameManager._currentCharacterCount % value == 2)
                {
                    GameManager._currentCharacterCount /= value;
                    GameManager._currentCharacterCount += 2;
                }

            }

        }
    }

    public class SaveLoad
    {
        public void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        public void SaveInteger(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
        public void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public string LoadString(string key)
        {
            return PlayerPrefs.GetString(key);
        }
        public int LoadInteger(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        public float LoadFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }


        public void Check()
        {
            if (!PlayerPrefs.HasKey("LastLevel"))
            {
                PlayerPrefs.SetInt("LastLevel", 5);
                PlayerPrefs.SetInt("Gem", 100);
                PlayerPrefs.SetInt("ActiveCap", -1);
                PlayerPrefs.SetInt("ActiveStick", -1);
                PlayerPrefs.SetInt("ActiveSkin", -1);
                PlayerPrefs.SetFloat("MenuMusic", 1);
                PlayerPrefs.SetFloat("MenuFX", 1);
                PlayerPrefs.SetFloat("GameMusic", 1);
                PlayerPrefs.SetString("Language", "ENG");
            }

        }
    }

    public class Data
    {
        public static List<ItemData> _itemData = new List<ItemData>();
    }
    [Serializable]
    public class ItemData
    {
        public int groupIndex;
        public int itemIndex;
        public string itemName;
        public int cost;
        public bool buyState;
    }
    public class DataController
    {


        public void Save(List<ItemData> _itemData)
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemData.gd");
            bf.Serialize(file, _itemData);
            file.Close();


        }
        
        List<ItemData> _itemDList;
        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemData.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemData.gd", FileMode.Open);
                _itemDList = (List<ItemData>)bf.Deserialize(file);
                file.Close();

            }
            else
            {
                Debug.LogError("Not Found Data");
            }
        }
        public void FirsTimeSave(List<ItemData> _itemData, List<LanguageData> _languageData)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemData.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemData.gd");
                bf.Serialize(file, _itemData);
                file.Close();
            }
            if (!File.Exists(Application.persistentDataPath + "/LanguageData.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/LanguageData.gd");
                bf.Serialize(file, _languageData);
                file.Close();
            }
        }

        public List<ItemData> GetList()
        {
            return _itemDList;

        }


        List<LanguageData> _languageDataIn;
        public void LanguageLoad()
        {
            if (File.Exists(Application.persistentDataPath + "/LanguageData.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/LanguageData.gd", FileMode.Open);
                _languageDataIn = (List<LanguageData>)bf.Deserialize(file);
                file.Close();

            }
            else
            {
                Debug.LogError("Not Found Data");
            }
        }

        public List<LanguageData> LanguageTransaction()
        {
            return _languageDataIn;
        }
    }


    //////
    [Serializable]
    public class LanguageData
    {
        public int index;
        public List<Language_TR> _languageTR = new List<Language_TR>();
        public List<Language_TR> _languageENG = new List<Language_TR>();
    }

    [Serializable]
    public class Language_TR
    {
        public string Text;
    }
  
}


