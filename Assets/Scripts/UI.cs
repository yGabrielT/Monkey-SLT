using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }

    public void Recome�ar()
    {
        if(WeaponManager.Instance.gameObject != null)
        {
            Destroy(WeaponManager.Instance.gameObject);
        }
        Jogar();
    }
}
