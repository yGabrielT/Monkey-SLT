using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private CameraController _cam;
    [SerializeField] float _shakeMag;
    [SerializeField] float _shakeDuration;
    [SerializeField] float RandomFactor = 0.5f;


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
            rbObjP.AddForce(RandomAccuracy(this.gameObject.transform.right, RandomFactor) * _bulletSpeed, ForceMode2D.Impulse);
            _cam.Shake(RandomAccuracy(-transform.right, RandomFactor), _shakeMag, _shakeDuration);
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

    private Vector3 RandomAccuracy(Vector3 weaponDir, float RandomnessFactor)
    {
        return weaponDir += new Vector3(Random.Range(-RandomnessFactor, RandomnessFactor), Random.Range(-RandomnessFactor, RandomnessFactor), 0);
    }
}
