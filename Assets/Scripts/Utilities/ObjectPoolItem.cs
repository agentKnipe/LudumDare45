using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ObjectPoolItem {
    public string Name;
    public int PoolSize;
    public GameObject ObjectToPool;
    public bool ShouldExpand;
}

