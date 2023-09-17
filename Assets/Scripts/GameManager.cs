using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject menu;
    [SerializeField] private int Arma1Custo;
    [SerializeField] private int Arma2Custo;
    [SerializeField] private int Arma3Custo;
    [SerializeField] private TextMeshProUGUI pontText;
    [SerializeField] private Button Arma1Botao;
    [SerializeField] private Button Arma2Botao;
    [SerializeField] private Button Arma3Botao;
    public int numeberOfKills;
    public int numberOfEnemies = 1;

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
        if(numeberOfKills == numberOfEnemies)
        {
            int sceneindex = SceneManager.GetActiveScene().buildIndex + 1;
            if(sceneindex < 4)
            {
                SceneManager.LoadScene(sceneindex);
            }
            
        }
        pontText.text = "Moedas: "+ WeaponManager.Instance.Pontuacao;
    }


    public void Arma2()
    {
        if (WeaponManager.Instance.Pontuacao >= Arma2Custo)
        {
            WeaponManager.Instance.Pontuacao -= Arma2Custo;
            Arma2Botao.interactable = false;
            Debug.Log("Arma 2");
            menu.SetActive(false);
            WeaponManager.Instance.weaponNumber = 2;
        }
        else
        {
            Debug.Log("Não há moedas");
        }

    }
    public void Arma3()
    {
        if (WeaponManager.Instance.Pontuacao >= Arma3Custo)
        {
            WeaponManager.Instance.Pontuacao -= Arma3Custo;
            Arma3Botao.interactable = false;
            Debug.Log("Arma 3");
            menu.SetActive(false);
            WeaponManager.Instance.weaponNumber = 3;
        }
        else
        {
            Debug.Log("Não há moedas");
        }

    }

    
}
