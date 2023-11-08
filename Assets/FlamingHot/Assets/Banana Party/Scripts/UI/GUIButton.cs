using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GUIButton : MonoBehaviour
{
    private Button btn;
    private Image img;
    [SerializeField] private Text txt;
    [SerializeField] private List<Sprite> sprStatusList = new List<Sprite>();
    [SerializeField] private List<string> desStatusList = new List<string>();
    [SerializeField] private Sprite enableSpr, disableSpr; 
    private int index = 0;
    private Action evt;
    private bool isDisable = false;

    private void Awake() 
    {
        btn = gameObject.GetComponent<Button>();
        img = gameObject.GetComponent<Image>();
    }

    private void Start() 
    {
        SetImage();
        SetTxt();
        btn.onClick.AddListener(ClickEvent);
    }

    private void ClickEvent()
    {
        if(isDisable)
            return;

        ChangeStatus();
        evt?.Invoke();
    }

    private void ChangeStatus()
    {
        index++;
        if(index >= desStatusList.Count)
            index = 0;

        SetImage();
        SetTxt();
    }

    public int GetStatusIndex()
    {
        return index;
    }

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
        Disable(!isShow);
    }

    public void Disable(bool disable)
    {
        if(!gameObject.activeSelf)
            return;
            
        isDisable = disable;
        btn.enabled = !disable;
        if(disableSpr == null || enableSpr == null)
        {
            if(btn == null)
                btn = GetComponent<Button>();

            btn.interactable = !disable;
        }
        else
        {
            if(img == null)
                img = GetComponent<Image>();

            Sprite spr = disable ? disableSpr : enableSpr;
            img.sprite = spr;
        }
    }

    public void SetEvent(Action evt)
    {
        this.evt = evt;
    }

    private void SetImage()
    {
        if(index >= sprStatusList.Count) 
            return;

        img.sprite = sprStatusList[index];
    }

    private void SetTxt()
    {
        if(index >= desStatusList.Count) 
            return;

        txt.text = desStatusList[index];
    }

    public void SetTxt(string str)
    {
        txt.text = str;
    }

    public void SetTextColor(Color color)
    {
        txt.color = color;
    }
    
}
