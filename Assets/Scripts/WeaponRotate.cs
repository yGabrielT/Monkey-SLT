using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    private Camera _playerCam;
    private Vector3 _mousePos;

    void Start()
    {
        _playerCam = Camera.main;
    }


    void Update()
    {
        _mousePos = _playerCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDir = _mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,angle);
    }
}
