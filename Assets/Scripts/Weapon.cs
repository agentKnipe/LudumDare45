using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IShootable, IDropable

{
    public GameObject Bullet;
    public bool IsDropable = false;
    public float Damage = 10f;
    public float RateOfFire = 1.0f;
    public float BulletSpeed = 25f;
    public Transform FirePoint;

    [SerializeField]
    GameObject DropPrefabObject;

    public float lastShot = 0f;

    GameObject IDropable.DropPrefab { get { return DropPrefabObject; } set { DropPrefabObject = value; } }

    // Start is called before the first frame update
    void Awake()
    {
        if(Bullet.GetComponent(typeof(BulletScript)) != null) {
            ((BulletScript)Bullet.GetComponent(typeof(BulletScript))).Damage = Damage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        lastShot += Time.deltaTime;
        ///Debug.Log("_lastShot");
    }

    public void Drop() {
        if (IsDropable) {
            Instantiate(DropPrefabObject, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }

    public virtual void Fire(Vector3 target) {
        if(lastShot >= RateOfFire) {
            var bullet = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
            bullet.transform.parent = gameObject.transform;

            //var forward = Vector3.Lerp(Vector3.zero, transform.forward * BulletSpeed, .8f);

            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(FirePoint.up * BulletSpeed, ForceMode2D.Impulse);

            GetComponent<AudioSource>().Play();
            lastShot = 0f;
        }
    }
}
