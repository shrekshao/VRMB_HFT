using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    GameObject mountedSwordsman;
    
    [Range(0.1f, 4f)]
    [SerializeField]
    float spawnXRangeMin = 1f;

    [Range(2f, 8f)]
    [SerializeField]
    float spawnXRangeMax = 5f;


    public int enemyCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;


    // Use this for initialization
    void Start () {
        StartCoroutine(SpawnEnemy());
    }


    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                //Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Vector3 spawnPosition = new Vector3( ( Random.value > 0.5f ? 1f: -1f) * Random.Range(spawnXRangeMin, spawnXRangeMax), 0f, -30f);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(mountedSwordsman, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
