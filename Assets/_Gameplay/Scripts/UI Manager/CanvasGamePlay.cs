using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] Text coinText;

    public override void Setup()
    {
        base.Setup();
        UpdateCoin(0);
    }

    public void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
    public void SettingsButton()
    {
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
    }
}
