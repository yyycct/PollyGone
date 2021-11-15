using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoxes : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] boxSpawnPoints;
    public GameObject boxPrefab;
    public GameObject boxParents;
    public static SpawnBoxes instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void spawnBox(int amount)
    {
        int spawnPoints = 0;
        Vector3 boxPosition;
        GameObject spawnBox;
        for (int i = 0; i < amount; i++)
        {
            spawnPoints = Random.Range(0, boxSpawnPoints.Length);
            spawnBox = boxSpawnPoints[spawnPoints];
            Vector3 tempPosition = spawnBox.transform.position;
            boxPosition = new Vector3(Random.Range(tempPosition.x - 30, tempPosition.x + 30), tempPosition.y,
                Random.Range(tempPosition.z - 5, tempPosition.z + 5));
            GameObject box = Instantiate(boxPrefab, boxPosition, Quaternion.identity);
            box.transform.RotateAround(tempPosition,Vector3.up, tempPosition.y);
            //box.transform.SetParent(boxParents.transform, true);
        }
    }
}
