using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    PlayerController playerController;

    [SerializeField] GameObject spawnPrefab; 
    [SerializeField] float spawnInterval = 0.01f;
    [SerializeField] Vector3 spawnFiled = new Vector3(3f, 0.95f, 0f);
    [SerializeField] int spawnCount = 500;


    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        StartCoroutine(SpawnObject(spawnInterval, spawnPrefab, spawnFiled));

      
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval = (1 / playerController.GetPosition()) * 100; 
    }

    private IEnumerator SpawnObject(float interval, GameObject spawnObject, Vector3 spawnPlace)
    {
        spawnFiled = new Vector3(Random.Range(2f, 3f), 1f, 0); 
        GameObject newObject = Instantiate(spawnPrefab, spawnFiled, Quaternion.identity);
        yield return new WaitForSeconds(interval * Time.time * Time.deltaTime);
        StartCoroutine(SpawnObject(spawnInterval, spawnPrefab, spawnFiled));
    }
}
