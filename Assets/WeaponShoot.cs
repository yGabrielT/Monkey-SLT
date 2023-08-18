using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _destroyTimer = 3f;
    [SerializeField] private bool _isEnemy = false;
    [SerializeField] private float _enemyCooldown = .5f;
    private float MaxCd;
    [HideInInspector]public bool inRange;

    void Start()
    {
        MaxCd = _enemyCooldown;
    }


    void Update()
    {
        
        if (Input.GetButtonDown("Fire1") && !_isEnemy)
        {
            var objP = Instantiate(_bulletPrefab, _spawnPos.position, Quaternion.identity);
            var rbObjP = objP.GetComponent<Rigidbody2D>();
            rbObjP.AddForce(this.gameObject.transform.right * _bulletSpeed, ForceMode2D.Impulse);
            Destroy(objP, _destroyTimer);
        }
        if(_isEnemy && inRange)
        {
            _enemyCooldown -= Time.deltaTime;
            if(_enemyCooldown <= 0f)
            {
                _enemyCooldown = MaxCd;
                var objE = Instantiate(_bulletPrefab, _spawnPos.position, Quaternion.identity);
                var rbObjE = objE.GetComponent<Rigidbody2D>();
                rbObjE.AddForce(this.gameObject.transform.right * _bulletSpeed, ForceMode2D.Impulse);
                Destroy(objE, _destroyTimer);
            }
            
        }
           
    }
}
