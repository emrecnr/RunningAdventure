using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{


    [SerializeField] public GameObject _targetPoint;

    public int _currentCharacterCount;

    [SerializeField] private List<GameObject> characters;

    private void Update()
    {

    }
    public void AICharacterControl(string value, Transform _position)
    {
        switch (value)
        {
            case "x2":
                int num = 0;

                foreach (var character in characters)
                {

                    if (num < _currentCharacterCount)
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
                _currentCharacterCount *= 2;
                break;


            case "+3":
                int num2 = 0;
                foreach (var character in characters)
                {

                    if (num2 < 3)
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
                _currentCharacterCount += 3;
                break;

            case "-4":

                if (_currentCharacterCount < 4)
                {
                    foreach (var character in characters)
                    {
                        character.transform.position = Vector3.zero;
                        character.SetActive(false);
                    }
                    _currentCharacterCount = 1;
                }
                else
                {
                    int num3 = 0;
                    foreach (var character in characters)
                    {

                        if (num3 != 4)
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
                    _currentCharacterCount -= 4;
                }

                break;

            case "/2":

                if (_currentCharacterCount <=2)
                {
                    foreach (var character in characters)
                    {
                        character.transform.position = Vector3.zero;
                        character.SetActive(false);
                    }
                    _currentCharacterCount = 1;
                }
                else
                {
                    int bolen = _currentCharacterCount / 2;
                    int num3 = 0;
                    foreach (var character in characters)
                    {

                        if (num3 != bolen)
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
                    if (_currentCharacterCount % 2 == 0)
                    {
                        _currentCharacterCount /= 2;
                    }
                    
                    else
                    {
                        _currentCharacterCount /= 2;
                        _currentCharacterCount++;
                    }
                   
                }

                break;


        }
    }
}
