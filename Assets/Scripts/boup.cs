using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class boup : MonoBehaviour
{
    [SerializeField] InputActionReference _myBoup;

    private void Start()
    {
        _myBoup.action.started += Action_started;
        _myBoup.action.performed += Action_started;
        _myBoup.action.canceled += Action_started;

    }


    private void Action_started(InputAction.CallbackContext aaa)
    {

        aaa.ReadValue<Vector2>();


    }
}
