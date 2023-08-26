using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    private RoomTemplates _templates;
    private int rand;
    public int openingDirection;
    private bool spawned = false;
    [SerializeField] private float waitTime = 5f;
    //1- Top
    //2- Right
    //3 - Bottom
    //4 - left

    private void Start()
    {
        Destroy(this.gameObject, waitTime);
        _templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.2f);
    }

    void Spawn()
    {
        if (!spawned && _templates.actualRoomsSpawned < _templates.MaxnumberOfRooms)
        {
            spawned = true;
            _templates.actualRoomsSpawned++;
            if (openingDirection == 3)
            {
                rand = Random.Range(0, _templates.topRooms.Length);
                Instantiate(_templates.topRooms[rand], transform.position, Quaternion.identity);
            }
            if (openingDirection == 4)
            {
                rand = Random.Range(0, _templates.rightRooms.Length);
                Instantiate(_templates.rightRooms[rand], transform.position, Quaternion.identity);
            }
            if (openingDirection == 1)
            {
                rand = Random.Range(0, _templates.bottomRooms.Length);
                Instantiate(_templates.bottomRooms[rand], transform.position, Quaternion.identity);
            }
            if (openingDirection == 2)
            {
                rand = Random.Range(0, _templates.leftRooms.Length);
                Instantiate(_templates.leftRooms[rand], transform.position, Quaternion.identity);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(_templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
