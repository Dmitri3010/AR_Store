using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    //float rotSpeed = 20;

    //void OnMouseDrag()
    //{
    //    float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
    //    //float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

    //    transform.RotateAround(Vector3.up, -rotX);
    //    //transform.RotateAround(Vector3.right, rotY);

    //    Debug.Log("Rotate");
    //}



    #region ROTATE
    private float _sensitivity = 0.5f;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation = Vector3.zero;
    private bool _isRotating;


    #endregion

    void Update()
    {
        if (_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            // apply rotation
            _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;

            // rotate
            gameObject.transform.Rotate(_rotation);

            // store new mouse position
            _mouseReference = Input.mousePosition;

            Debug.Log("Rotate");
        }
    }

    void OnMouseDown()
    {
        // rotating flag
        _isRotating = true;

        // store mouse position
        _mouseReference = Input.mousePosition;

        Debug.Log("Down");

    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }

}

