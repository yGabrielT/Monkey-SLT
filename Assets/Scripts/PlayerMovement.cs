using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDamageable
{

    private bool isFlipped = false;
    [SerializeField] private Animator _animPlayer;
    [SerializeField] private float MOVE_BASE_SPEED;
    private float _movementSpeed;
    Vector2 _direction;

    [SerializeField] private TextMeshProUGUI _vidaText;
    [SerializeField] Transform _weapon;

    public float _currentHealth { get; set; }
    [field: SerializeField] public float _maxHealth { get; set; } = 100f;
    public bool _isKnockbacked { get; set; }
    public Rigidbody2D _rb { get; set; }
    [field: SerializeField] public float _knockbackDelay { get; set; } = .5f;
    [field: SerializeField] public float _knockbackForce { get; set; } = 10f;

    SpriteRenderer _spr;

    void Start()
    {
        _spr = GetComponentInChildren<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;
    }

    void Update()
    {
        if(_rb.velocity != Vector2.zero){
            _animPlayer.SetBool("isWalking", true);
        }
        else{
            _animPlayer.SetBool("isWalking", false);
        }

        _vidaText.text = "Vida: " + _currentHealth;
        if (!_isKnockbacked)
        {
            Move();
            GetInput();
        }
        
    }

    private void Move()
    {

        _rb.velocity = _direction.normalized * _movementSpeed * MOVE_BASE_SPEED;
    }

    private void GetInput()
    {
            var inputX = Input.GetAxis("Horizontal");
            var inputY = Input.GetAxis("Vertical");
            
            _direction = new Vector2(inputX, inputY);
            _movementSpeed = Mathf.Clamp(_direction.magnitude, 0f, 1f);

            if(inputX <0)
            {
                _spr.flipX = true;
            }
            else{
                _spr.flipX = false;
            }

    }

    public IEnumerator KnockBack(Vector2 dir)
    {
        Debug.Log("Botao Espa�o Pressionado");
        _isKnockbacked = true;

        float _elapsedTime = 0f;
        while(_elapsedTime < _knockbackDelay)
        {
            _elapsedTime += Time.fixedDeltaTime;


            //_playerRb.AddForce(weapon.forward.normalized * _knockbackForce, ForceMode2D.Impulse);
            _rb.velocity = dir * _knockbackForce;
            yield return new WaitForFixedUpdate();
        }

        _isKnockbacked = false;
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == ("Bullet"))
        {
            TakeDamage(5f, (transform.position - other.gameObject.transform.position).normalized);

            Destroy(other.gameObject);
        }
    }


    public void TakeDamage(float damage, Vector2 bulletDir)
    {
        _currentHealth -= damage;
        Debug.Log("Dano");
        if (_currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        StartCoroutine(KnockBack(bulletDir));
    }


}
