using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStore : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _menu.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _menu.SetActive(false);
    }
}
