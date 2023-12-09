using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    // https://www.youtube.com/watch?v=oLT4k-lrnwg&list=PLyBYG1JGBcd009lc1ZfX9ZN5oVUW7AFVy&index=3&ab_channel=TheKiwiCoder
    public EnemyHealth health;

    public void OnRaycastHit(Weapon weapon, Vector3 direction)
    {
        // Damage that is taken by the enemy should be the property of the weapon;
        health.TakeDamage(weapon.weaponDamage, direction);
    }
}
