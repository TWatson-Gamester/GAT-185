using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerTimePickup : RollerPickup, IDestructable
{
    [SerializeField] int Time = 10;
    public void Destroyed()
    {
        BallGameManager.Instance.GameTime += Time;
    }
}
