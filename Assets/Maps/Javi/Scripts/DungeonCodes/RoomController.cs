using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string[] levelNames = new string[]
    {
        "Beginning",
        "Forest",
        "City",
        "Building",
        "Sewers"
    };

    public enum LevelType
    {
        Beginning,
        Forest,
        City,
        Building,
        Sewers
    }

    public LevelType levelType;

    string currentWorldName;

    RoomInfo currentLoadRoomData;

    Room currRoom;
    public Room CurrentRoom { get { return currRoom; } }

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;
    bool spawnedSpecialRooms = false;
    bool loadedSpecialRooms = false;
    bool updatedRooms = false;

    public SoundControllerScript sc;

    private HashSet<string> possibleSceneNames = new HashSet<string>();

    void Awake()
    {
        instance = this;
        currentWorldName = levelNames[(int)levelType];
    }

    void Start()
    {
        while (sc == null)
        {
            sc = GameObject.FindGameObjectWithTag("SoundControl").GetComponent<SoundControllerScript>();
        }
        PlayLevelMusic();
        if (levelType == LevelType.Beginning)
        {
            LoadRoom("Start", 0, 0);
            spawnedSpecialRooms = true;
            loadedSpecialRooms = true;
        }
        RemoveExcessLoadings();
        GenerateSceneNames();
    }

    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedSpecialRooms && !loadedSpecialRooms)
            {
                StartCoroutine(SpawnSpecialRooms());
            }
            else if (spawnedSpecialRooms && loadedSpecialRooms && !updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveConnectedDoors();
                }
                updatedRooms = true;
                DespawnRooms();
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    void RemoveExcessLoadings()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene roomScene = SceneManager.GetSceneAt(i);
            if (roomScene.name == "Loading")
            {
                AsyncOperation unload = SceneManager.UnloadSceneAsync(roomScene);
            }
        }
    }

    void DespawnRooms()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene roomScene = SceneManager.GetSceneAt(i);
            if (possibleSceneNames.Contains(roomScene.name))
            {
                AsyncOperation unload = SceneManager.UnloadSceneAsync(roomScene);
            }
        }
    }

    void GenerateSceneNames()
    {
        string baseWorldName = currentWorldName + "_";
        possibleSceneNames.Add(baseWorldName + "Start");
        possibleSceneNames.Add(baseWorldName + "Finish");
        possibleSceneNames.Add(baseWorldName + "Shop");
        possibleSceneNames.Add(baseWorldName + "Chest");
        for (int i = 1; i <= 8; i++)
        {
            possibleSceneNames.Add(baseWorldName + i);
        }
    }

    IEnumerator SpawnSpecialRooms()
    {
        spawnedSpecialRooms = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            int shopRoom, chestRoom, exitRoom;
            exitRoom = loadedRooms.Count - 1;
            shopRoom = Random.Range(1, loadedRooms.Count - 2);
            do
            {
                chestRoom = Random.Range(1, loadedRooms.Count - 2);
            } while (shopRoom == chestRoom);
            Room shop, chest, exit;
            shop = loadedRooms[shopRoom];
            chest = loadedRooms[chestRoom];
            exit = loadedRooms[exitRoom];
            generateSpecialRoom(shop, "Shop");
            generateSpecialRoom(chest, "Chest");
            generateSpecialRoom(exit, "Finish");
            loadedSpecialRooms = true;
        }
    }

    void generateSpecialRoom(Room room, string roomName)
    {
        Room tempRoom = new Room(room.X, room.Y);
        Destroy(room.gameObject);
        var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
        loadedRooms.Remove(roomToRemove);
        LoadRoom(roomName, tempRoom.X, tempRoom.Y);
    }

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + "_" + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }

    }

    public void RegisterRoom(Room room)
    {
        if(!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.Width,
                currentLoadRoomData.Y * room.Height,
                0
            );

            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if(loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }

            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }
    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRoom = room;
    }

    void PlayLevelMusic()
    {
        switch (levelType)
        {
            case LevelType.Beginning:
                sc.playStartMusic();
                break;
            case LevelType.Forest:
                sc.playForestMusic();
                break;
            case LevelType.City:
                sc.playCityMusic();
                break;
            case LevelType.Building:
                sc.playHouseMusic();
                break;
            case LevelType.Sewers:
                sc.playSewersMusic();
                break;
        }
    }

    public void StopLevelMusic()
    {
        switch (levelType)
        {
            case LevelType.Beginning:
                sc.StopStartMusic();
                break;
            case LevelType.Forest:
                sc.stopForestMusic();
                break;
            case LevelType.City:
                sc.stopCityMusic();
                break;
            case LevelType.Building:
                sc.stopHouseMusic();
                break;
            case LevelType.Sewers:
                sc.stopSewersMusic();
                break;
        }
    }
}
