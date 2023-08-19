using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _enemyRb;
    [SerializeField] private float _enemyHealth = 100f;
    [SerializeField] private float ENEMY_MOVE_BASE_SPEED = 5f;
    private Transform _playerTarget;
    [SerializeField] private float _aggroRange = 5f;
    [SerializeField] private GameObject _pivotWeapon;
    private WeaponShoot _weapon;

    void Start()
    {
        _weapon = GetComponentInChildren<WeaponShoot>();
        _enemyRb = GetComponent<Rigidbody2D>();
        _playerTarget = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (ChechIfIsInRange(_aggroRange, _playerTarget) || _weapon.inRange)
        {
            Vector3 direction = _playerTarget.position - transform.position;
            _enemyRb.velocity = direction.normalized * ENEMY_MOVE_BASE_SPEED;

            _weapon.inRange = true;
            Vector3 directionWeapon =  transform.position - _playerTarget.position;
            float angle = Mathf.Atan2(directionWeapon.y, directionWeapon.x) * Mathf.Rad2Deg;
            _pivotWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private bool ChechIfIsInRange(float aggroRange, Transform target)
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if(distance > aggroRange)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _aggroRange);
    }

    private void TakeDamage(float damage)
    {
        if (!_weapon.inRange)
        {
            _weapon.inRange = true;
        }
        if (_enemyHealth <= 100)
        {
            _enemyHealth -= damage;
            Debug.Log("Dano");
        }
        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            TakeDamage(20);
        }
    }
}
