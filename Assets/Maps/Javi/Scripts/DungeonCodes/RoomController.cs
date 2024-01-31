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
    //string currentWorldName = "Forest";

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

    void Awake()
    {
        instance = this;
        currentWorldName = levelNames[(int)levelType];
        // GameObject player = GameObject.FindGameObjectWithTag("Player");
        // sc = player.GetComponent<PlayerStateMachine>().SoundController;
        // while (sc == null)
        // {
            // sc = player.GetComponent<PlayerStateMachine>().SoundController;
        // }
    }

    void Start()
    {
        while (sc == null)
        {
            sc = GameObject.FindGameObjectWithTag("SoundControl").GetComponent<SoundControllerScript>();
        }
        // sc = GameObject.Find("SoundController").GetComponent<SoundControllerScript>();
        PlayLevelMusic();
        if (levelType == LevelType.Beginning)
        {
            // Debug.Log("Starting room");
            LoadRoom("Start", 0, 0);
            // LoadRoom("Start", 1, 0);
            // LoadRoom("Start", 0, 1);
            // LoadRoom("Start", 1, 1);

            
        }
        // Debug.Log(loadedRooms.Count);
        //LoadRoom("Start", 0, 0);
        //LoadRoom("Empty", 1, 0);
        //LoadRoom("Empty", -1, 0);
        //LoadRoom("Empty", 0, -1);
        //LoadRoom("Empty", 2, 0);
        //LoadRoom("Empty", 2, -1);
        //LoadRoom("Empty", 2, 1);
        //LoadRoom("Empty", -1, -1);
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
                // DespawnRoomScenes();
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    // void DespawnRoomScenes()
    // {
    //     SceneManager.UnloadSceneAsync(levelNames[(int)levelType] + "_Start");
    //     SceneManager.UnloadSceneAsync(levelNames[(int)levelType] + "_Chest");
    //     SceneManager.UnloadSceneAsync(levelNames[(int)levelType] + "_Shop");
    //     SceneManager.UnloadSceneAsync(levelNames[(int)levelType] + "_Finish");
    //     int despawnedRooms = 0;
    //     string sceneName = levelNames[(int)levelType] + "_";
    //     for (int i = 4; i < 10; i++)
    //     {
    //         // string sceneName = levelNames[(int)levelType] + "_" + i;
    //         try
    //         {
    //             // string sceneName = levelNames[(int)levelType] + "_" + i;
    //             do
    //             {
    //                 // Debug.Log(SceneManager.GetSceneByName(sceneName).isLoaded);
    //                 // SceneManager.UnloadSceneAsync(i);
    //                 Scene unloadScene = SceneManager.GetSceneByName(sceneName + i);
    //                 SceneManager.UnloadSceneAsync(unloadScene);
    //                 despawnedRooms++;
    //                 Debug.Log(SceneManager.GetSceneByBuildIndex(i).name);
    //                 Debug.Log("Unloading " + sceneName);
    //                 // Scene
    //             } while (SceneManager.GetSceneByName(sceneName + i) != null);
    //             // SceneManager.UnloadSceneAsync(levelNames[(int)levelType] + "_" + i);
    //         }
    //         catch (System.Exception)
    //         {
    //             Debug.LogError("No empty room " + i);
    //         }
    //     }
    //     Debug.Log("Despawned " + despawnedRooms + " rooms");
    // }

    IEnumerator SpawnSpecialRooms()
    {
        spawnedSpecialRooms = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            //Room finalRoom = loadedRooms[loadedRooms.Count - 1];
            int shopRoom, chestRoom, exitRoom;
            exitRoom = loadedRooms.Count - 1;
            shopRoom = Random.Range(1, loadedRooms.Count - 2);
            do
            {
                chestRoom = Random.Range(1, loadedRooms.Count - 2);
            } while (shopRoom == chestRoom);
            //Room tempRoom = new Room(finalRoom.X, finalRoom.Y);
            //Destroy(finalRoom.gameObject);
            //var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            //loadedRooms.Remove(roomToRemove);
            //LoadRoom("Finish", tempRoom.X, tempRoom.Y);
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
        // Room room = loadedRooms[index];
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
        // Debug.Log("Loading room: " + name + " " + x + ", " + y);

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

        // Debug.Log("Loading room " + info.name + " " + info.X + ", " + info.Y);
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
        // if (sc == null)
        // {
        //     Debug.Log("SoundController is null");
        // }
        switch (levelType)
        {
            case LevelType.Beginning:
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
