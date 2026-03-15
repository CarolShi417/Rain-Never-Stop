using UnityEngine;
using System.Collections.Generic;

public class LightGroupGeneration : MonoBehaviour
{
    public GameObject lightPrefab;
    public Transform[] spawnPoints;

    public void SpawnRandomLights(int count)
    {
        if (spawnPoints.Length < count)
        {
            Debug.LogError("生成点数量不足");
            return;
        }

        List<int> usedIndex = new List<int>();

        for (int i = 0; i < count; i++)
        {
            int index;

            do
            {
                index = Random.Range(0, spawnPoints.Length);
            }
            while (usedIndex.Contains(index));

            usedIndex.Add(index);

            Instantiate(
                lightPrefab,
                spawnPoints[index].position,
                Quaternion.identity
            );
        }
    }
}