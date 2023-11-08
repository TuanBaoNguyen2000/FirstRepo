using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnReplay : BaseButton
{
    [SerializeField] protected string sceneName = "GamePlay";

    protected override void OnClick()
    {
        //Debug.LogWarning("click");
        SceneManager.LoadScene(this.sceneName);
    }
}
