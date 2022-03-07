using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Item.Type type;
    [SerializeField] GameObject pickupPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Inventory>(out Inventory inventory))
        {
            if (inventory.Additem(type))
            {
                if (pickupPrefab)
                {
                    Instantiate(pickupPrefab, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }
}
