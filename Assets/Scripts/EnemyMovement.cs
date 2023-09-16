using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IDamageable
{

    [SerializeField] private float ENEMY_MOVE_BASE_SPEED = 5f;
    private Transform _playerTarget;
    [SerializeField] private float _aggroRange = 5f;
    [SerializeField] private GameObject _pivotWeapon;
    private WeaponShoot _weapon;

    private Animator _animEnemy;
    private SpriteRenderer _sprEnemy;

    public Rigidbody2D _rb { get; set; }
    public float _currentHealth { get; set; }
    [field: SerializeField] public float _maxHealth { get; set; } = 100f;
    public bool _isKnockbacked { get; set; }
    [field: SerializeField] public float _knockbackDelay { get; set; } = .5f;
    [field: SerializeField] public float _knockbackForce { get; set; } = 10f;

    void Start()
    {
        _sprEnemy = GetComponentInChildren<SpriteRenderer>();
        _animEnemy = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;
        _weapon = GetComponentInChildren<WeaponShoot>();
        _rb = GetComponent<Rigidbody2D>();
        _playerTarget = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        EnemyAnimation();

        if (!_isKnockbacked)
        {
            FollowPlayer();
        }
        
    }

    private void EnemyAnimation()
    {
        if(_rb.velocity != Vector2.zero){
            _animEnemy.SetBool("isWalking", true);
        }
        else
        {
            _animEnemy.SetBool("isWalking", false);
        }
    }

    private void FlipEnemySprite()
    {
        if (_rb.velocity.x < 0)
        {
            _sprEnemy.flipX = true;
        }
        else
        {
            _sprEnemy.flipX = false;
        }
    }

    private void FollowPlayer()
    {
        if (ChechIfIsInRange(_aggroRange, _playerTarget) || _weapon.inRange)
        {
            Vector3 direction = _playerTarget.position - transform.position;
            _rb.velocity = direction.normalized * ENEMY_MOVE_BASE_SPEED;

            _weapon.inRange = true;
            Vector3 directionWeapon = transform.position - _playerTarget.position;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("BulletPlayer"))
        {
            TakeDamage(20f, (transform.position - other.gameObject.transform.position).normalized);
            Destroy(other.gameObject);
        }
    }


    public void TakeDamage(float damage, Vector2 bulletDir)
    {
        _weapon.inRange = true;
        _currentHealth -= damage;
        Debug.Log("Dano");
        if (_currentHealth <= 0)
        {
            GameManager.Instance.Pontuacao += 10;
            Destroy(this.gameObject);
        }

        StartCoroutine(KnockBack(bulletDir));
    }

    public IEnumerator KnockBack(Vector2 dir)
    {
        Debug.Log("Botao Espaço Pressionado");
        _isKnockbacked = true;

        float _elapsedTime = 0f;
        while (_elapsedTime < _knockbackDelay)
        {
            _elapsedTime += Time.fixedDeltaTime;


            //_playerRb.AddForce(weapon.forward.normalized * _knockbackForce, ForceMode2D.Impulse);
            _rb.velocity = dir * _knockbackForce;
            yield return new WaitForFixedUpdate();
        }

        _isKnockbacked = false;
    }
}
