using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropable
{
    GameObject DropPrefab { get; set; }

    void Drop();
}
