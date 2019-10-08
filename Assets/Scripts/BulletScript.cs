using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Damage;

    private void Update() {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0) {
            Destroy(gameObject);
        }
            
    }

    void OnCollisionEnter2D(Collision2D col) {
        IDamageable hit = (IDamageable)col.gameObject.GetComponent(typeof(IDamageable));
        if (hit != null) {
            hit.DoHit(Damage);
        }

        if (!col.gameObject.CompareTag("Bullet1") && !col.gameObject.CompareTag("Bullet2")) {
            //Do Hit Animation
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        
    }
}
