using UnityEngine;
using UnityEngine.Events;

public class MainView : View
{
    private LevelUpDialog _levelUpDialog;
    private UnityAction<CurrencyData> _updateCurrencyAction;

    protected override void Init()
    {
        base.Init();

        var canvasList = FindObjectsOfType<Canvas>();
        foreach(var canvas in canvasList)
        {
            var levelUpDialog = canvas.transform.Find("LevelUpDialog");
            if(levelUpDialog != null)
            {
                _levelUpDialog = levelUpDialog.GetComponent<LevelUpDialog>();
            }
        }

        GetButton("LevelUpButton").onClick.AddListener(OnLevelUpButtonClick);
        GetButton("MoreButton").onClick.AddListener(OnMoreButtonClick);

        GetImage("More").gameObject.SetActive(false);

        _updateCurrencyAction = UpdateMoreCurreny;
    }

    public override void OpenView()
    {
        base.OpenView();
        var userData = Global.Datas.User;
        GetText("GoldCount").text = $"{userData.GetCurrency(CurrencyType.Gold)}";
        GetText("DiamondCount").text = $"{userData.GetCurrency(CurrencyType.Diamond)}";
        GetText("RubyCount").text = $"{userData.GetCurrency(CurrencyType.Ruby)}";
        GetText("EXPCount").text = $"{userData.GetCurrency(CurrencyType.Exp)}";
        Global.Datas.User.CurrencyUpdate.AddListener(_updateCurrencyAction);
    }

    public override void CloseView()
    {
        base.CloseView();
        Global.Datas.User.CurrencyUpdate.RemoveListener(_updateCurrencyAction);
    }

    private void OnLevelUpButtonClick()
    {
        Global.UI.OpenDialog(_levelUpDialog);
    }

    private void OnButton1Click()
    {

    }

    private void OnButton2Click()
    {

    }

    private void OnButton3Click()
    {

    }

    private void OnButton4Click()
    {

    }

    private void OnMoreButtonClick()
    {
        GetImage("More").gameObject.SetActive(GetImage("More").gameObject.activeSelf == false);
    }

    private void UpdateMoreCurreny(CurrencyData currencyData)
    {
        switch (currencyData.Id)
        {
            case (int)CurrencyType.Gold:
                GetText("GoldCount").text = $"{currencyData.Count}";
                break;
            case (int)CurrencyType.Diamond:
                GetText("DiamondCount").text = $"{currencyData.Count}";
                break;
            case (int)CurrencyType.Ruby:
                GetText("RubyCount").text = $"{currencyData.Count}";
                break;
            case (int)CurrencyType.Exp:
                GetText("EXPCount").text = $"{currencyData.Count}";
                break;
        }
    }
}
