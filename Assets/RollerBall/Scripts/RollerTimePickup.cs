using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerTimePickup : Pickup, IDestructable
{
    [SerializeField] int Time = 10;
    public void Destroyed()
    {
        BallGameManager.Instance.GameTime += Time;
    }
}
