using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    public GameObject diamond;
    public bool spawn = true;
    private float spawnXPos;
    private const float spawnYPos = -5.5f;
    public float minSpawnXPos;
    public float maxSpawnXPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnDiamonds(4));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnDiamonds(float spawnTimer)
    {
        while (spawn)
        {
            spawnXPos = Random.Range(minSpawnXPos, maxSpawnXPos);

            Instantiate(diamond, new Vector3(spawnXPos, spawnYPos,0), Quaternion.identity);
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
