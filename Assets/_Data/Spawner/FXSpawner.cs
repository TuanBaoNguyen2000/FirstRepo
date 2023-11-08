using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSpawner : Spawner
{
    private static FXSpawner instance;
    public static FXSpawner Instance => instance;

    public static string enemy1Death = "Enemy1_death";
    public static string enemy2Death = "Enemy2_death";
    public static string explosion1 = "Explosion_1";
    public static string playerDeath = "Player_death";
    public static string bossDeath = "Boss_death";
    public static string xSign = "X_Sign";

    protected override void Awake()
    {
        base.Awake();
        if (FXSpawner.instance != null) Debug.LogError("Only 1 FXSpawner allow to exist");
        FXSpawner.instance = this;
    }
}
