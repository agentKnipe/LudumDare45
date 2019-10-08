using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IDamageable {
    public float ScaleMin;
    public float ScaleMax;
    [SerializeField]
    private float hitPoints;
    [SerializeField]
    private float armor;

    public float HitPoints { get => hitPoints; set => hitPoints = value; }
    public float Armor { get => armor; set => armor = value; }


    // Start is called before the first frame update
    void Start() {
        var scale = UnityEngine.Random.Range(ScaleMin, ScaleMax);
        var scaleVector = new Vector3(scale, scale, scale);

        transform.localScale = scaleVector;

    }

    // Update is called once per frame
    void Update() {

    }

    public void DoHit(float damage) {
        hitPoints -= damage;

        if (hitPoints <= 0) {
            //do destroy animation
            Destroy(gameObject);
        }
    }
}
