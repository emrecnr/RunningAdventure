using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CameraController _camera;
    [SerializeField] private GameObject _battlePoint;
    private float _moveSpeed = 1f;

    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _point;

    public bool isFinish;

    private void Start()
    {
        float distance = Vector3.Distance(transform.position, _point.transform.position);
        _slider.maxValue = distance;
    }

    private void FixedUpdate()
    {
        if (!isFinish)
        {
           transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);

        }
    }

    private void Update()
    {


        if (isFinish)
        {
            transform.position = Vector3.Lerp(transform.position, _battlePoint.transform.position, 0.015f);
            if (_slider.value != 0 )
            {
                _slider.value -= 0.1f;
            }

        }
        else
        {
            //Slider
            float distance = Vector3.Distance(transform.position, _point.transform.position);
            _slider.value = distance;
            //
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f);
                }
                if (Input.GetAxis("Mouse X") > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Multiplication") || other.CompareTag("Addition") || other.CompareTag("Substraction") || other.CompareTag("Division"))
        {
            int num = int.Parse(other.name);
            _gameManager.AICharacterControl(other.tag, num, other.transform);
            Debug.Log("carpti");
        }
        else if (other.CompareTag("FinishTrigger"))
        {
            _camera.isFinish = true;
            _gameManager.TriggerEnemy();
            isFinish= true;
            Debug.Log("!!! Finish !!!");
        }
        else if (other.CompareTag("OtherCharacter"))
        {
            _gameManager.characters.Add(other.gameObject);
            GameManager._currentCharacterCount++;
            other.gameObject.tag = "AICharacter";
                
        }
    }
    
    //Direk takilmalirini engellemek icin
    private void OnCollisionEnter(Collision collision)
    {       
        if (collision.gameObject.CompareTag("Stick")|| collision.gameObject.CompareTag("Obstacles")|| collision.gameObject.CompareTag("fan"))
        {
            if (transform.position.x > 0)
            {
                transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
            }
            
        }
    }

}
