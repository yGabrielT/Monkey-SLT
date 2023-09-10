using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject menu;
    public float Pontuacao = 0f;
    [SerializeField] private float Arma1Custo;
    [SerializeField] private float Arma2Custo;
    [SerializeField] private float Arma3Custo;
    [SerializeField] private TextMeshProUGUI pontText;
    [SerializeField] private Button Arma1Botao;
    [SerializeField] private Button Arma2Botao;
    [SerializeField] private Button Arma3Botao;

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

   

    void Update()
    {
        pontText.text = "Moedas: "+ Pontuacao;
    }

    public void Arma1()
    {
        if (Pontuacao >= Arma1Custo)
        {
            Pontuacao -= Arma1Custo;
            Arma1Botao.interactable = false;
            Debug.Log("Arma 1");
            menu.SetActive(false);
        }
        else
        {
            Debug.Log("Não há moedas");
        }
        
            
    }
    public void Arma2()
    {
        if (Pontuacao >= Arma2Custo)
        {
            Pontuacao -= Arma2Custo;
            Arma2Botao.interactable = false;
            Debug.Log("Arma 2");
            menu.SetActive(false);
        }
        else
        {
            Debug.Log("Não há moedas");
        }

    }
    public void Arma3()
    {
        if (Pontuacao >= Arma3Custo)
        {
            Pontuacao -= Arma3Custo;
            Arma3Botao.interactable = false;
            Debug.Log("Arma 3");
            menu.SetActive(false);
        }
        else
        {
            Debug.Log("Não há moedas");
        }

    }

    
}
