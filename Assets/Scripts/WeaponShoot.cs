using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private bool _isEnemy = false;
    [SerializeField] private float _enemyCooldown = .5f;
    private float Cd;
    [HideInInspector]public bool inRange;
    [SerializeField] private CameraController _cam;
    [SerializeField] float _shakeMag;
    [SerializeField] float _shakeDuration;
    [SerializeField] float RandomFactor = 0.5f;


    [SerializeField] private SOGun GunScriptable;
    private GameObject _bulletPrefab;
    private float _bulletSpeed = 10f;
    private float _destroyTimer = 3f;
    private float _fireRate = .3f;

    private float fireTimer;


    void Start()
    {
        //Pegar variaveis do Scriptable Object
        
        _bulletPrefab = GunScriptable.BulletPrefab;
        _bulletSpeed = GunScriptable.BulletSpeed;
        _destroyTimer = GunScriptable.DestroyTimer;
        _fireRate = GunScriptable.FireRate;

        Cd = _enemyCooldown;
        
    }


    void Update()
    {
        
        if (Input.GetMouseButton(0) && !_isEnemy && fireTimer <= 0f)
        {
            fireTimer = _fireRate;
            Shoot(GunScriptable.isShotgun, GunScriptable.ShotgunSpread);
        }
        if(fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
        }
        if(_isEnemy && inRange)
        {
            
            if(Cd <= 0f && _bulletPrefab != null)
            {
                Cd = _enemyCooldown;
                var objE = Instantiate(_bulletPrefab, _spawnPos.position, Quaternion.identity);
                var rbObjE = objE.GetComponent<Rigidbody2D>();
                rbObjE.AddForce(this.gameObject.transform.right * _bulletSpeed, ForceMode2D.Impulse);
                Destroy(objE, _destroyTimer);
            }
            else{
                Cd -= Time.deltaTime;
            }
            
        }
           
    }

    private Vector3 RandomAccuracy(Vector3 weaponDir, float RandomnessFactor)
    {
        return weaponDir += new Vector3(Random.Range(-RandomnessFactor, RandomnessFactor), Random.Range(-RandomnessFactor, RandomnessFactor), 0);
    }

    private void Shoot(bool Shotgun, float spread){
        if(!Shotgun){
            var objP1 = Instantiate(_bulletPrefab, _spawnPos.position, Quaternion.identity);
            var rbObjP1 = objP1.GetComponent<Rigidbody2D>();
            rbObjP1.AddForce(RandomAccuracy(this.gameObject.transform.right, RandomFactor) * _bulletSpeed, ForceMode2D.Impulse);
            _cam.Shake(RandomAccuracy(-transform.right, RandomFactor), _shakeMag, _shakeDuration);
            Destroy(objP1, _destroyTimer);
            return;      
        }
        else{
            var objP2 = Instantiate(_bulletPrefab, _spawnPos.position, _bulletPrefab.transform.rotation * Quaternion.Euler(0f, -GunScriptable.ShotgunSpread, 0f));
            var rbObjP2 = objP2.GetComponent<Rigidbody2D>();
            rbObjP2.AddForce(RandomAccuracy(this.gameObject.transform.right, RandomFactor) * _bulletSpeed, ForceMode2D.Impulse);
            _cam.Shake(RandomAccuracy(-transform.right, RandomFactor), _shakeMag, _shakeDuration);
            Destroy(objP2, _destroyTimer);

            var objP3 = Instantiate(_bulletPrefab, _spawnPos.position, _bulletPrefab.transform.rotation);
            var rbObjP3 = objP3.GetComponent<Rigidbody2D>();
            rbObjP3.AddForce(RandomAccuracy(this.gameObject.transform.right, RandomFactor) * _bulletSpeed, ForceMode2D.Impulse);
            _cam.Shake(RandomAccuracy(-transform.right, RandomFactor), _shakeMag, _shakeDuration);
            Destroy(objP3, _destroyTimer);

            var objP4 = Instantiate(_bulletPrefab, _spawnPos.position, _bulletPrefab.transform.rotation * Quaternion.Euler(0f, GunScriptable.ShotgunSpread, 0f));
            var rbObjP4 = objP4.GetComponent<Rigidbody2D>();
            rbObjP4.AddForce(RandomAccuracy(this.gameObject.transform.right, RandomFactor) * _bulletSpeed, ForceMode2D.Impulse);
            _cam.Shake(RandomAccuracy(-transform.right, RandomFactor), _shakeMag, _shakeDuration);
            Destroy(objP4, _destroyTimer);

        }
              
    }
}
