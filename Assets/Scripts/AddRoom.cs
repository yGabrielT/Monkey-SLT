using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;
    [SerializeField] bool isEntryRoom;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
        if (!isEntryRoom)
        {
            for (int i = 0; i < RandNum(); i++)
            {
                GameManager.Instance.numberOfEnemies++;
                Instantiate(templates.enemy, RandPos(), Quaternion.identity);
            }
        }
        
    }

    private Vector3 RandPos()
    {
        return new Vector3(transform.position.x + Random.Range(-5f, 5f), transform.position.y + Random.Range(-5f, 5f), transform.position.z);
    }

    private int RandNum()
    {
        return Random.Range(1, 4);
    }
}

