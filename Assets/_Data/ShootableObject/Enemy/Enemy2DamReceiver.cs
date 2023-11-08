using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2DamReceiver : ShootableObjectDamReceiver
{
    protected override string GetOnDeadFXName()
    {
        return FXSpawner.enemy2Death;
    }

}
