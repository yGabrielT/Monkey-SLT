using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private float MOVE_BASE_SPEED;
    private float _movementSpeed;
    Vector2 _direction;
    private bool _isKnockBacked = false;
    [SerializeField] private float _knockbackDelay = 0.5f;
    [SerializeField] private float _knockbackForce = 3f;
    [SerializeField] private float playerHealth = 100f;
    [SerializeField] Transform weapon;


    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(KnockBack());
        }
        
        Move();
        GetInput();
    }
    
    private void Move()
    {
        if(!_isKnockBacked)
        _playerRb.velocity = _direction.normalized * _movementSpeed * MOVE_BASE_SPEED;
    }

    private void GetInput()
    {
            var inputX = Input.GetAxis("Horizontal");
            var inputY = Input.GetAxis("Vertical");
            _direction = new Vector2(inputX, inputY);
            _movementSpeed = Mathf.Clamp(_direction.magnitude, 0.0f, 1.0f);
    }

    private IEnumerator KnockBack()
    {
        Debug.Log("Botao Espa�o Pressionado");
        _isKnockBacked = true;
        _movementSpeed = 0f;
        _playerRb.AddForce(weapon.forward * _knockbackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_knockbackDelay);
        _isKnockBacked = false;
        _playerRb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == ("Bullet"))
        {
            TakeDamage(20f);
            Destroy(other.gameObject);
        }
    }

    private void TakeDamage(float damage)
    {
        playerHealth -= damage;
        Debug.Log("Dano");
        if (playerHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
