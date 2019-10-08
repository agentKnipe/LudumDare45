using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    public float Speed = 1;
    [SerializeField]
    private float hitPoints;
    [SerializeField]
    private float armor;

    public float HitPoints { get => hitPoints; set => hitPoints = value; }
    public float Armor { get => armor; set => armor = value; }

    public float SightAngle = 90;
    public float SightLines = 10;
    public float SightRange = 25;

    public GameObject[] Weapons;
    public Rigidbody2D Rb;

    bool _TargetVisible = true;
    Vector3 _TargetLocation;

    // Start is called before the first frame update
    void Awake(){
        int addedCount = 0;
        var childCount = transform.childCount;
        Weapons = new GameObject[childCount];

        for (int i = 0; i < childCount; i++) {
            var child = transform.GetChild(i);
            
            if(child.childCount > 0) {
                Weapons[addedCount] = child.transform.GetChild(0).gameObject;

                addedCount++;
            }
        }
    }

    void Update() {
        if(transform.position.x > 150 || transform.position.x < -150 || transform.position.y > 150 || transform.position.y < -150) {
            DestroyEnemy();
        }

        var hit = RayCastSweep();

        if (hit.collider != null && hit.distance <= SightRange) {
            _TargetVisible = true;
            _TargetLocation = hit.collider.transform.position;
            _TargetLocation.z = -1;

            var lookDir = _TargetLocation - transform.position;
            lookDir.z = -1;

            var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);

            for (int i = 0; i < Weapons.Length; i++) { 
                if(Weapons[i] != null) {
                    ((IShootable)Weapons[i].GetComponent(typeof(Weapon))).Fire(_TargetLocation);
                }
            }
        }
        else {
            _TargetVisible = false;
            //_TargetLocation = transform.up + Vector3.one * Speed * Time.deltaTime;
        }

        if (_TargetVisible) {
            var moveTowards = Vector2.MoveTowards(transform.position, _TargetLocation, Speed * Time.deltaTime);
            transform.position = moveTowards;
        }
        else {
            Vector2 direction = transform.TransformDirection(transform.right * Speed);
            var forward = GetForward();
            //direction.z = -1;
            var vector = (Vector3)(Vector2.up * Speed * Time.deltaTime);

            transform.position += (forward * Speed * Time.deltaTime);
        }

    }

    Vector3 GetForward() {
        var direction = Vector2.up;
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        Vector3 forward = new Vector3(
            direction.x * cos - direction.y * sin,
            direction.x * sin + direction.y * cos,
            -1f);

        Vector3 position = transform.position;
        Debug.DrawLine(position, position + (Vector3)direction, Color.red);
        Debug.DrawLine(position, position + forward, Color.green);

        return forward;
    }


    void OnDrawGizmos() {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector2.up) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }

    public void DoHit(float damage) {
        HitPoints -= damage;

        if(HitPoints <= 0) {
            GameManager.instance.UpdateScore();
            // do die animation
            //hide mesh
            DestroyEnemy();
        }
    }

    RaycastHit2D RayCastSweep() {
        var front = transform.GetChild(0);
        var start = (Vector2)front.position;
        var target = Vector2.zero;

        var startAngle = 20.0f;
        var endAngle = 160.0f;

        var increment = 2f; // (float)(SightAngle / SightLines);

        for(var i = startAngle; i <= endAngle; i += increment) {
            var result = transform.position + Quaternion.AngleAxis(i, transform.forward) * transform.right * SightRange;
            target = new Vector2(result.x, result.y);

            var hit = Physics2D.Raycast(start, target);
            //Debug.DrawRay(start, target, Color.red, .02f);
            if(hit.collider != null && hit.collider.CompareTag("Player")) {
                return hit;
            }
        }

        return new RaycastHit2D();
    }

    private Vector2 RandomVector2(float angle, float angleMin) {
        float random = Random.value * angle + angleMin;
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

    void DestroyEnemy() {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
