using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace Math
{
    public class LibraryM : MonoBehaviour
    {
        public static void Multiplication(int value, List<GameObject> characters, Transform _position, List<GameObject> spawnParticles)
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
        public static void Addition(int value, List<GameObject> characters, Transform _position, List<GameObject> spawnParticles)
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
        public static void Substraction(int value, List<GameObject> characters, List<GameObject> destroyParticles)
        {
            if (GameManager._currentCharacterCount < value)
            {
                foreach (var character in characters)
                {
                    foreach(var destroy in destroyParticles)
                    {
                        if (!destroy.activeInHierarchy)
                        {
                            Vector3 offset = new Vector3(character.transform.position.x, 0.25f, character.transform.position.z);
                            destroy.SetActive(true);
                            destroy.transform.position = offset;
                            destroy.GetComponent<ParticleSystem>().Play();
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
        public static void Division(int value, List<GameObject> characters, List<GameObject> destroyParticles)
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

}


