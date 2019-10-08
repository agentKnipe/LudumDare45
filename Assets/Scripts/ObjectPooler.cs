using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstasnce;

    public List<GameObject> PooledObjects;
    public ObjectPoolItem[] ItemsToPool;

    private void Awake() {
        SharedInstasnce = this;
    }

    // Start is called before the first frame update
    void Start(){
        PooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem item in ItemsToPool) {
            for (int i = 0; i < item.PoolSize; i++) {
                GameObject obj = (GameObject)Instantiate(item.ObjectToPool);
                obj.SetActive(false);
                PooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag) {
        for (int i = 0; i < PooledObjects.Count; i++) {
            if (!PooledObjects[i].activeInHierarchy && PooledObjects[i].tag == tag) {
                return PooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in ItemsToPool) {
            if (item.ObjectToPool.tag == tag) {
                if (item.ShouldExpand) {
                    GameObject obj = (GameObject)Instantiate(item.ObjectToPool);
                    obj.SetActive(false);
                    PooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}
