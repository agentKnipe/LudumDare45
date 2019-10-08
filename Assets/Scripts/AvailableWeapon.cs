using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableWeapon : MonoBehaviour, IPickupable
{
    public float MaxScale;
    public float MinScale;
    public float ScaleAmount = .2f;
    public float ScaleSpeed = .15f;
    public float RotationRate;

    public GameObject GameObjectPrefab;

    Vector3 _scale;

    float _scaleTimer = 0f;

    public GameObject ObjectPrefab {
        get {
            return GameObjectPrefab;
        }
    }

    void Start() {
        _scale = new Vector3(ScaleAmount, ScaleAmount, ScaleAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if(_scaleTimer >= ScaleSpeed) {
            if (transform.localScale.x >= MaxScale || transform.localScale.x <= MinScale) {
                _scale = _scale * -1f;
            }

            transform.localScale += _scale;

            _scaleTimer = 0f;
        }
        else {
            _scaleTimer += Time.deltaTime;
        }

        transform.Rotate(Vector3.forward * (RotationRate * Time.deltaTime));
    }

    public void PickedUp() {
        //pickup Animation
        Destroy(gameObject);
    }
}
