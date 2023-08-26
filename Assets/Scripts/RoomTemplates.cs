using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public int MaxnumberOfRooms;
    public int actualRoomsSpawned;

    public List<GameObject> rooms;

    public float waitTime = 10f;
    private bool _spawnedBoss;
    public GameObject boss;
    public GameObject enemy;

    private void Update()
    {
        if(waitTime <= 0 && !_spawnedBoss)
        {
            Instantiate(boss, rooms[rooms.Count-1].transform.position, Quaternion.identity);
            _spawnedBoss = true;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

}
