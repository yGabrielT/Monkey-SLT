using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    Rigidbody2D _rb { get; set; }
    float _currentHealth {  get; set; }
    float _maxHealth { get; set; }

    bool _isKnockbacked { get; set; }

    float _knockbackDelay { get; set; }

    float _knockbackForce { get; set; }

    void TakeDamage(float damage, Vector2 bulletDir);

    IEnumerator KnockBack(Vector2 dir);
}
