using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMushroom : MonoBehaviour
{
    public GameObject[] mushPrefabs;
    public GameObject[] mushSpawnPoints;
    // Start is called before the first frame update
    public static SpawnMushroom instance;
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
    public void spawnMush(int amount)
    {
        int spawnPoints = 0;
        Vector3 mushPosition;
        GameObject spawnBox;
        for (int i = 0; i < amount; i++)
        {
            spawnPoints = Random.Range(0, mushSpawnPoints.Length);
            spawnBox = mushSpawnPoints[spawnPoints];
            Vector3 tempPosition = spawnBox.transform.position;
            Vector3 tempScale = spawnBox.transform.localScale;
            mushPosition = new Vector3(Random.Range(tempPosition.x - tempScale.x/2, tempPosition.x + tempScale.x/2), tempPosition.y,
                Random.Range(tempPosition.z - tempScale.z / 2, tempPosition.z + tempScale.z / 2));
            int randomMush = Random.Range(0, 100);
            GameObject mush= mushPrefabs[0];
            if (randomMush < 85)
            {
                mush = mushPrefabs[0];
            }
            else if(randomMush < 95)
            {
                mush = mushPrefabs[1];
            }
            else
            {
                mush = mushPrefabs[2];
            }
            Instantiate(mush, mushPosition, Quaternion.identity);
        }
    }
}
