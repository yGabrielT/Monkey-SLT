using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _smoothTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _playerTransform.position, Time.deltaTime * _smoothTime);
    }
}
