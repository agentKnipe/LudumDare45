using System;
using UnityEngine;

[Serializable]
public class ObstacleType
{
    public string Name;
    public float MinHeight;
    public float MaxHeight;

    public GameObject[] ObjectPrefabs;


    private float _range {
        get {
            return MaxHeight - MinHeight;
        }
    }

    public GameObject GetPrefab(float height) {
        var indexRange = _range / ObjectPrefabs.Length;

        for(int i = 0; i < ObjectPrefabs.Length; i++){
            var min = (indexRange * i) + MinHeight;
            var max = min + indexRange;

            if(height > min && height <= max) {
                return ObjectPrefabs[i];
            }
        }

        return ObjectPrefabs[0];
    }
}
