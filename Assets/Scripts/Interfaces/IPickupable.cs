using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    GameObject ObjectPrefab { get; }

    void PickedUp();
}
