using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachments : MonoBehaviour
{
    public GameObject[] _attachmentPoints;

    void Start() {
        var childCount = transform.childCount;
        _attachmentPoints = new GameObject[childCount];

        for (int i = 0; i < childCount; i++) {
            _attachmentPoints[i] = transform.GetChild(i).gameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D hit) {
        //figure out attachments...
        var availableWeapon = (IPickupable)hit.GetComponent(typeof(IPickupable));
        
        AttachPrefab(availableWeapon.ObjectPrefab);
        
        availableWeapon.PickedUp();
        
        Debug.Log(hit.name);

    }

    void AttachPrefab(GameObject weaponPrefab) {
        for (int i = 0; i < _attachmentPoints.Length; i++) {
            if(_attachmentPoints[i].transform.childCount == 0) {
                var weapon = Instantiate(weaponPrefab, _attachmentPoints[i].transform.position, _attachmentPoints[i].transform.rotation);
                weapon.transform.parent = _attachmentPoints[i].transform;

                AddWeapon(weapon);
                return;
            }
        }
    }
    
    void AddWeapon(GameObject weapon) {
        var player = (Player)gameObject.GetComponent(typeof(Player));
    
        player.Weapons.Add(weapon);
    }
}
