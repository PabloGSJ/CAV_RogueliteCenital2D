using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;
    public int Height;
    public int X;
    public int Y;

    private bool updatedDoors = false;

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public Corridor leftCorridor;
    public Corridor rightCorridor;
    public Corridor topCorridor;
    public Corridor bottomCorridor;


    public List<Door> doors = new List<Door>();
    public List<Corridor> corridors = new List<Corridor>();
    public List<Door> lockableDoors = new List<Door>();

    void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.LogError("You pressed play in the wrong scene!");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach(Door d in ds)
        {
            doors.Add(d);
            switch(d.doorType)
            {
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.top:
                    topDoor = d;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                    break;
            }
        }

        Corridor[] cs = GetComponentsInChildren<Corridor>();
        foreach(Corridor c in cs)
        {
            corridors.Add(c);
            switch(c.corridorType)
            {
                case Corridor.CorridorType.left:
                    leftCorridor = c;
                    break;
                case Corridor.CorridorType.right:
                    rightCorridor = c;
                    break;
                case Corridor.CorridorType.top:
                    topCorridor = c;
                    break;
                case Corridor.CorridorType.bottom:
                    bottomCorridor = c;
                    break;
            }
        }

        RoomController.instance.RegisterRoom(this);
    }

    void Update()
    {
        if(name.Contains("Finish") && !updatedDoors)
        {
            RemoveConnectedDoors();
            updatedDoors = true;
        }
    }

    public void RemoveConnectedDoors()
    {
        foreach(Corridor corridor in corridors)
        {
            switch(corridor.corridorType)
            {
                case Corridor.CorridorType.left:
                    if(GetLeft() == null)
                    {
                        corridor.gameObject.SetActive(false);
                    }
                    else
                    {
                        lockableDoors.Add(leftDoor);
                        leftDoor.gameObject.SetActive(false);
                    }
                    break;
                case Corridor.CorridorType.right:
                    if(GetRight() == null)
                    {
                        corridor.gameObject.SetActive(false);
                    }
                    else
                    {
                        lockableDoors.Add(rightDoor);
                        rightDoor.gameObject.SetActive(false);
                    }
                    break;
                case Corridor.CorridorType.top:
                    if(GetTop() == null)
                    {
                        corridor.gameObject.SetActive(false);
                    }
                    else
                    {
                        lockableDoors.Add(topDoor);
                        topDoor.gameObject.SetActive(false);
                    }
                    break;
                case Corridor.CorridorType.bottom:
                    if(GetBottom() == null)
                    {
                        corridor.gameObject.SetActive(false);
                    }
                    else
                    {
                        lockableDoors.Add(bottomDoor);
                        bottomDoor.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    public void LockDoors()
    {
        foreach(Door door in lockableDoors)
        {
            door.gameObject.SetActive(true);
        }
    }

    public void UnlockDoors()
    {
        foreach(Door door in lockableDoors)
        {
            door.gameObject.SetActive(false);
        }
    }

    public Room GetLeft()
    {
        if(RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }
    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }
    public Room GetTop()
    {
        if(RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }
    public Room GetBottom()
    {
        if(RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
