using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using SimpleJSON;

[System.Serializable]
class GlobalLevelData
{
    public List<LevelData> levels;
}

[System.Serializable]
class LevelData
{
    //public Dictionary<int, >
    public List<SpawnData> spawns;
}

[System.Serializable]
class SpawnData
{
    public float distance; // player position on the track when spawning

    //public Vector3 spawnPosition { get; set; }
    //public float[] position { get; set; }
    public float positionX;

    public String name;  // enemy name

    // other parameter of enemy
}


public class WaveEnemeySpawner : MonoBehaviour {

    public string levelFileName = "levels";


    static GlobalLevelData globalLevelData;
    //static JSONNode levelJSON = JSON.Parse(
    //static GlobalLevelData globalLevelData = JsonUtility.FromJson<GlobalLevelData>(
    //GlobalLevelData globalLevelData = JsonUtility.FromJson<GlobalLevelData>(
    //    @"{
    //        ""levels"": [
    //            {
    //                ""spawns"": 
    //                    [
    //                        {
    //                            ""distance"": -50,
    //                            ""positionX"": 2,
    //                            ""name"": ""archer""
    //                        }
    //                        ,
    //                        {
    //                            ""distance"": -50,
    //                            ""positionX"": -2,
    //                            ""name"": ""archer""
    //                        }
    //                        ,
    //                        {
    //                            ""distance"": -53,
    //                            ""positionX"": 4,
    //                            ""name"": ""archer""
    //                        }
    //                        ,
    //                        {
    //                            ""distance"": -53,
    //                            ""positionX"": -4,
    //                            ""name"": ""archer""
    //                        } 
    //                        ,
    //                        {
    //                            ""distance"": -70,
    //                            ""positionX"": 1,
    //                            ""name"": ""swordsman""
    //                        }

    //                    ]

    //            }

    //        ]
    //    }
    //    "
    //    );


    [SerializeField]
    GameObject mountedEnemyPrefab;

    [SerializeField]
    GameObject target;


    Dictionary<string, SoldierType> enemyTypeDict = new Dictionary<string, SoldierType>()
    {
        {"archer", SoldierType.Bowman },
        {"swordsman", SoldierType.Swordsman }
    };



    int level = 0;
    int curSpawnId = 0;

    LevelData curLevel = null;
    SpawnData curSpawn = null;


    // Use this for initialization
    void Start () {
        //LoadLevelJsonFile(levelFileName + ".json");

        //Debug.Log(globalLevelData);
        //Debug.Log(a);

        //globalLevelData = JsonUtility.FromJson<GlobalLevelData>(ReadJsonFile("/Levels/GlobalLevelData.json"));
        TextAsset ta = Resources.Load("GlobalLevelData") as TextAsset;
        globalLevelData = JsonUtility.FromJson<GlobalLevelData>(ta.text);

        curLevel = globalLevelData.levels[level];

        if (curSpawnId < curLevel.spawns.Count)
        {
            curSpawn = curLevel.spawns[curSpawnId];
        }

        EventDelegateManager.instance.restartLevelDelegate += OnRestartLevel;


    }
	
	// Update is called once per frame
	void Update () {
        while (curSpawn != null)
        {
            // currently negative z direction
            if (target.transform.position.z <= curSpawn.distance)
            {
                spawnEnemey(curSpawn);
                curSpawnId++;
                
                if (curSpawnId < curLevel.spawns.Count)
                {
                    curSpawn = curLevel.spawns[curSpawnId];
                }
                else
                {
                    // level ends
                    curSpawn = null;
                }
            }
            else
            {
                break;
            }
        }
    }


    void spawnEnemey(SpawnData spawnData)
    {
        SoldierType type = enemyTypeDict[spawnData.name];
        Vector3 spawnPosition = new Vector3(spawnData.positionX, transform.position.y, transform.position.z);
        GameObject go = Instantiate(mountedEnemyPrefab, spawnPosition, Quaternion.identity) as GameObject;

        go.GetComponentInChildren<Enemy>().SetSoldierType(type);
    }


    public void OnRestartLevel()
    {
        curSpawn = null;
        curSpawnId = 0;

        if (curSpawnId < curLevel.spawns.Count)
        {
            curSpawn = curLevel.spawns[curSpawnId];
        }
    }



    //public void LoadLevelJsonFile(string json_file)
    //{
    //    levelJSON = JSON.Parse(ReadJsonFile(json_file));
        
    //}

    public string ReadJsonFile(string filename)
    {
        string dataAsString = "";

        try
        {
            // open text file
            StreamReader textReader = File.OpenText(Application.dataPath + filename);

            // read contents
            dataAsString = textReader.ReadToEnd();

            // close file
            textReader.Close();

        }
        catch (Exception e)
        {
            //			display/set e.Message error message here if you wish  ...
            Debug.Log(e.Message);
        }

        // return contents
        return dataAsString;
    }
}
