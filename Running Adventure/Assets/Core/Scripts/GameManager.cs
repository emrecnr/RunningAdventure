using Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{


    [SerializeField] public GameObject _targetPoint;

    public static int _currentCharacterCount;

    [SerializeField] private List<GameObject> characters;

    private void Update()
    {

    }
    public void AICharacterControl(string type, int value, Transform _position)
    {
        switch (type)
        {
            case "Multiplication":
                LibraryM.Multiplication(value, characters, _position);
                break;


            case "Addition":
                LibraryM.Addition(value, characters, _position);
                break;

            case "Substraction":

                LibraryM.Substraction(value, characters);

                break;

            case "Division":

                LibraryM.Division(value, characters);

                break;
        }
    }
}
