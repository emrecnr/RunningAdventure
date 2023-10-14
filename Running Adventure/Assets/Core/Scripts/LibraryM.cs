using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace Math
{
    public class LibraryM : MonoBehaviour
    {
        public static void Multiplication(int value, List<GameObject> characters, Transform _position)
        {
            int loopTime = (GameManager._currentCharacterCount * value) - GameManager._currentCharacterCount;
            int num = 0;

            foreach (var character in characters)
            {

                if (num < loopTime)
                {
                    if (!character.activeInHierarchy)
                    {
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
        public static void Addition(int value, List<GameObject> characters, Transform _position)
        {
            int num2 = 0;
            foreach (var character in characters)
            {

                if (num2 < value)
                {
                    if (!character.activeInHierarchy)
                    {
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
        public static void Substraction(int value, List<GameObject> characters)
        {
            if (GameManager._currentCharacterCount < value)
            {
                foreach (var character in characters)
                {
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
        public static void Division(int value, List<GameObject> characters)
        {
            if (GameManager._currentCharacterCount <= value)
            {
                foreach (var character in characters)
                {
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


