using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] private SOGun weapon1;
    [SerializeField] private SOGun weapon2;
    [SerializeField] private SOGun weapon3;
    public int Pontuacao;
    public SOGun actualWeapon;
    public static WeaponManager Instance;

    public int weaponNumber = 1;

    //1 - pistola
    //2 - shotgun
    //3 - minigun

    void Awake()
    {

        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(weaponNumber == 1)
        {
            actualWeapon = weapon1;
        }
        if (weaponNumber == 2)
        {
            actualWeapon = weapon2;
        }
        if (weaponNumber == 3)
        {
            actualWeapon = weapon3;
        }
    }
}
