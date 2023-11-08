using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamReceiver : ShootableObjectDamReceiver
{
    protected override string GetOnDeadFXName()
    {
        return FXSpawner.bossDeath;
    }
}
