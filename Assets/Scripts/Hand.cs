using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Input_Controls _input;
    private Camera _camera;
    private Vector3 _screenMousePosition = new Vector3();
    private Vector3 _InGameMousePosition = new Vector3();

    void MoveHand()
    {
        _screenMousePosition = _input.Action_Map.TapPosition.ReadValue<Vector2>();
        _screenMousePosition.z = 7; //рассто€ние от камеры до руки, пока хардкод, но нужно исправить
        _InGameMousePosition = _camera.ScreenToWorldPoint(_screenMousePosition);
        Debug.Log(_screenMousePosition);
        Debug.Log(_InGameMousePosition);
        //Ёкономи€ ресурса на выделении буферного вектора, вместо этого используетс€ _InGameMousePosition
        _InGameMousePosition.y = gameObject.transform.position.y;
        _InGameMousePosition.z = gameObject.transform.position.z;

        gameObject.transform.position = _InGameMousePosition;
    }

    private void Awake()
    {
        _input = new Input_Controls();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

 
    void Update()
    {
        MoveHand(); 
    }
}
