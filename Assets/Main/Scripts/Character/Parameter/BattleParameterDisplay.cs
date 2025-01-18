using UnityEngine;

public class BattleParameterDisplay : MonoBehaviour
{
    public BattleParameterBase BattleParameter;
    public GameObject[] BattleParameterDisplays;

    /// <summary>
    /// BattleParameterをセットする
    /// </summary>
    /// <param name="battleParameter"></param>
    public void SetBattleParameter(BattleParameterBase battleParameter)
    {
        BattleParameter = battleParameter;
    }

    /// <summary>
    /// 現在のBattleParameterに応じてUIを更新する
    /// </summary>
    public void UpdateDisplay()
    {
        if (BattleParameter != null)
        {
            BattleParameterDisplays[0].GetComponent<TMPro.TextMeshProUGUI>().text = BattleParameter.MaxHP.ToString();
            BattleParameterDisplays[1].GetComponent<TMPro.TextMeshProUGUI>().text = BattleParameter.AttackPower.ToString();
            BattleParameterDisplays[2].GetComponent<TMPro.TextMeshProUGUI>().text = BattleParameter.DefensePower.ToString();
        }
    }
}