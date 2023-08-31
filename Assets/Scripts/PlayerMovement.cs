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
    [SerializeField] private float _maxPlayerHealth = 200f;
    private float healthPlayer;


    void Start()
    {
        healthPlayer = _maxPlayerHealth;
    }

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
        Debug.Log("Botao Espaço Pressionado");
        _isKnockBacked = true;
        _movementSpeed = 0f;
        _playerRb.AddForce(Vector2.right * _knockbackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_knockbackDelay);
        _isKnockBacked = false;
        _playerRb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == ("Bullet"))
        {
            GameManager.Instance.TakeDamage(healthPlayer, 20f, this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
