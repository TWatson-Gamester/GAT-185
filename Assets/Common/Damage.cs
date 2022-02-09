using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] float damage = 0;
    [SerializeField] bool oneTime = true;


    private void OnTriggerEnter(Collider other)
    {
        if (!oneTime) return;

        if(other.gameObject.TryGetComponent<Health>(out Health outHealth))
        {
            outHealth.Damage(damage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (oneTime) return;

        if (other.gameObject.TryGetComponent<Health>(out Health outHealth))
        {
            outHealth.Damage(damage * Time.deltaTime);
        }
    }
}
