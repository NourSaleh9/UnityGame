using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenrate : MonoBehaviour
{

    private SpriteRenderer floorSr;
    private SpriteRenderer wallsSr;

    private SpriteRenderer dooru;


    RoomAsset room;

    void Start()
    {
        GameObject roomAsset = GameObject.Find("RoomAsset");
        room = roomAsset.GetComponent<RoomAsset>();

        floorSr = transform.Find("floor").transform.gameObject.GetComponent<SpriteRenderer>();
        floorSr.sprite = room.floors[Random.Range(0, room.floors.Count)];

        wallsSr = transform.Find("roomWalls").transform.gameObject.GetComponent<SpriteRenderer>();
        wallsSr.sprite = room.roomWalls[Random.Range(0, room.roomWalls.Count)];

        dooru = transform.Find("DoorT").transform.Find("door bricks up").gameObject.GetComponent<SpriteRenderer>();
        if(dooru != null)
            dooru.sprite = room.doorUp[Random.Range(0, room.doorUp.Count)];
        dooru = transform.Find("DoorB").transform.Find("door bricks up").gameObject.GetComponent<SpriteRenderer>();
        if (dooru != null)
            dooru.sprite = room.doorUp[Random.Range(0, room.doorUp.Count)];
        dooru = transform.Find("DoorL").transform.Find("door bricks up").gameObject.GetComponent<SpriteRenderer>();
        if (dooru != null)
            dooru.sprite = room.doorUp[Random.Range(0, room.doorUp.Count)];
        dooru = transform.Find("DoorR").transform.Find("door bricks up").gameObject.GetComponent<SpriteRenderer>();
        if (dooru != null)
            dooru.sprite = room.doorUp[Random.Range(0, room.doorUp.Count)];



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
