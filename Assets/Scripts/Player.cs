using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float hitPoints;
    [SerializeField]
    private float armor;

    public float HitPoints { get => hitPoints; set => hitPoints = value; }
    public float Armor { get => armor; set => armor = value; }

    public List<GameObject> Weapons;

    float _initialHealth;
    // Start is called before the first frame update
    void Start()
    {
        _initialHealth = HitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")) {
            foreach(var weapon in Weapons) {
                ((IShootable)weapon.GetComponent(typeof(IShootable))).Fire(transform.up);
            }
        }
    }

    void IDamageable.DoHit(float damage) {
        HitPoints -= damage;

        GameManager.instance.UpdateHealth(HitPoints / _initialHealth);

        if(HitPoints <= 0){
            GameManager.instance.EndGame();
        };

        
    }
}
