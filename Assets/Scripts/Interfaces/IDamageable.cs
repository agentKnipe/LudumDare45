using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable{
    float HitPoints { get; set; }
    float Armor { get; set; }

    void DoHit(float damage);
    
}
