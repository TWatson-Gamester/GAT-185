using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] float damage = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Health>(out Health outHealth))
        {
            outHealth.Damage(damage);
        }
/*        if (gameObject.TryGetComponent<Health>(out Health inHealth) && other.gameObject.TryGetComponent<Damage>(out Damage inDamage))
        {
            inHealth.Damage(inDamage.damage);
        }*/
    }
}
