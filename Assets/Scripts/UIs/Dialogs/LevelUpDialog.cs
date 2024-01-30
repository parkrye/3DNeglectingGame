using System.Collections.Generic;

public class LevelUpDialog : Dialog
{
    private Dictionary<string, Template> _templates = new Dictionary<string, Template>();

    protected override void Init()
    {
        base.Init();
    }

    private void Start()
    {
        var userData = Global.Datas.UserData;

        var levelSlotTemplate = GetTemplate("LevelFrameTemplate");
        levelSlotTemplate.GetText("LevelText").text = $"Lv {userData.ActorData.Level}";
        levelSlotTemplate.GetImage("LevelBar").fillAmount = userData.GetCurrency(CurrencyType.Exp) / userData.ActorData.Level * 10;
        _templates.Add("LV", levelSlotTemplate);
        levelSlotTemplate.GetButton("LevelUpButton").onClick.AddListener(() => OnClickLevelUpButton("LV"));

        var statSlotTemplate = GetTemplate("StatFrameTemplate");
        var content = GetContent("StatScroll");

        var instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "HP";
        instant.GetText("StatDescription").text = $"{userData.ActorData.Hp}";
        instant.GetText("StatUpCost").text = $"{userData.ActorData.Hp * 5}";
        _templates.Add("HP", instant);
        instant.GetButton("StatUpButton").onClick.AddListener(() => OnClickLevelUpButton("HP"));

        instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "Move Speed";
        instant.GetText("StatDescription").text = $"{userData.ActorData.MoveSpeed}";
        instant.GetText("StatUpCost").text = $"{userData.ActorData.MoveSpeed * 5}";
        _templates.Add("MS", instant);
        instant.GetButton("StatUpButton").onClick.AddListener(() => OnClickLevelUpButton("MS"));

        instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "Attack Damage";
        instant.GetText("StatDescription").text = $"{userData.ActorData.AttackDamage}";
        instant.GetText("StatUpCost").text = $"{userData.ActorData.AttackDamage * 5}";
        _templates.Add("AD", instant);
        instant.GetButton("StatUpButton").onClick.AddListener(() => OnClickLevelUpButton("AD"));

        instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "Attack Speed";
        instant.GetText("StatDescription").text = $"{userData.ActorData.AttackSpeed}";
        instant.GetText("StatUpCost").text = $"{userData.ActorData.AttackSpeed * 5}";
        _templates.Add("AS", instant);
        instant.GetButton("StatUpButton").onClick.AddListener(() => OnClickLevelUpButton("AS"));

        statSlotTemplate.gameObject.SetActive(false);
    }

    private void UpdateCost()
    {
        var userData = Global.Datas.UserData;

        _templates["LV"].GetImage("LevelBar").fillAmount = userData.GetCurrency(CurrencyType.Exp) / userData.ActorData.Level * 10;
        _templates["HP"].GetText("StatUpCost").text = $"{userData.ActorData.Hp * 5}";
        _templates["MS"].GetText("StatUpCost").text = $"{userData.ActorData.MoveSpeed * 5}";
        _templates["AD"].GetText("StatUpCost").text = $"{userData.ActorData.AttackDamage * 5}";
        _templates["AS"].GetText("StatUpCost").text = $"{userData.ActorData.AttackSpeed * 5}";
    }

    private void OnClickLevelUpButton(string name)
    {
        var userData = Global.Datas.UserData;

        switch (name)
        {
            default:
                break;
            case "LV":
                if (userData.TryUseCurrency(CurrencyType.Exp, userData.ActorData.Level * 10) == true)
                {
                    userData.ActorData.Level += 1;
                    UpdateCost();
                }
                break;
            case "HP":
                if (userData.TryUseCurrency(CurrencyType.Ruby, userData.ActorData.Hp * 5) == true)
                {
                    userData.ActorData.Hp += 1;
                    UpdateCost();
                }
                break;
            case "MS":
                if (userData.TryUseCurrency(CurrencyType.Ruby, userData.ActorData.MoveSpeed * 5) == true)
                {
                    userData.ActorData.MoveSpeed += 1;
                    UpdateCost();
                }
                break;
            case "AD":
                if (userData.TryUseCurrency(CurrencyType.Ruby, userData.ActorData.AttackDamage * 5) == true)
                {
                    userData.ActorData.AttackDamage += 1;
                    UpdateCost();
                }
                break;
            case "AS":
                if (userData.TryUseCurrency(CurrencyType.Ruby, userData.ActorData.AttackSpeed * 5) == true)
                {
                    userData.ActorData.AttackSpeed += 1;
                    UpdateCost();
                }
                break;
        }
    }
}
