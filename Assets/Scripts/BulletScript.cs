using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] float _lifetime = 5f;

    float _age;

    void Update()
    {
        if (_age < _lifetime) {
            _age += Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision other) {
        var damageReciever = other.gameObject.GetComponent<IDamageable>();
        damageReciever?.Damage(_damage);

        if (other.gameObject.tag == "Wall") {

        }
    }
}
