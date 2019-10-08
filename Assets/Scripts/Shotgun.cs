using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon, IShootable
{
    GameObject[] _firePoints;

    public GameObject ObjectPrefab => throw new System.NotImplementedException();

    // Start is called before the first frame update
    void Start()
    {
        _firePoints = new GameObject[transform.GetChild(0).transform.childCount];

        for (int i = 0; i < transform.GetChild(0).transform.childCount; i++) {
            _firePoints[i] = transform.GetChild(0).transform.GetChild(i).gameObject; ;
        }
    }

    void Update() {
        lastShot += Time.deltaTime;
    }

    public virtual void Fire(Vector3 target) {
        if (lastShot >= RateOfFire) {
            for (int i = 0; i < _firePoints.Length; i++) {
                var bullet = Instantiate(Bullet, _firePoints[i].transform.position, _firePoints[i].transform.rotation);
                bullet.transform.parent = gameObject.transform;

                var rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(_firePoints[i].transform.up * BulletSpeed, ForceMode2D.Impulse);
                
            }

            GetComponent<AudioSource>().Play();

            lastShot = 0f;
        }
    }

    public void PickedUp() {
        throw new System.NotImplementedException();
    }
}
