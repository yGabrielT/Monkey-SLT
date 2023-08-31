using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if(Instance != this && Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        
    }

    public void TakeDamage(float health, float damage, GameObject obj)
    {
        health -= damage;
        Debug.Log("Dano");
        if (health <= 0)
        {
            Destroy(obj);
        }
    }

    void Update()
    {
        
    }
}
