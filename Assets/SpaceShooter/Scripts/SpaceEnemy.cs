using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemy : MonoBehaviour, IDestructable
{
    [SerializeField] SpaceWeapon spaceWeapon;
    [SerializeField] float minFireTime;
    [SerializeField] float maxFireTime;

    private float timer;
    public int points;

    public void Destroyed()
    {
        GameManager.Instance.Score += points;
    }

    void Start()
    {
        timer = Random.Range(minFireTime, maxFireTime);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(minFireTime, maxFireTime);
            spaceWeapon.Fire();
        }
    }
}
