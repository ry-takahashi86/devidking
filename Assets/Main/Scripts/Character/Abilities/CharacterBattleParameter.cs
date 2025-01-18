using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class CharacterBattleParameter : CharacterAbility, MMEventListener<MMInventoryEvent>
{
    [Header("Battle Parameter")]
    public BattleParameter InitialBattleParameter;
    public BattleParameterBase BattleParameter;

    // 必須コンポーネント
    protected CharacterInventoryAllEquip _equipInventoryAllEquip;

    protected override void Start()
    {
        // 初期パラメータを反映
        InitialBattleParameter.Data.CopyTo(BattleParameter);

        // 必須コンポーネントを取得
        _equipInventoryAllEquip = GetComponent<CharacterInventoryAllEquip>();
    }

    /// <summary>
    /// BattleParameter を BattleParameterDisplay にセットする
    /// </summary>
    /// <param name="battleParameter"></param>
    public void SendBattleParameter(BattleParameterBase battleParameter)
    {
        BattleParameterDisplay display = FindFirstObjectByType<BattleParameterDisplay>();
        if (display != null)
        {
            display.SetBattleParameter(battleParameter);
            display.UpdateDisplay();
        }
    }

    /// <summary>
    /// イベントアイテムから対象インベントリを特定して格納されているアイテムを装備する
    /// </summary>
    /// <param name="battleParameter"></param>
    public void BattleParameterUpdateEquipments(InventoryItem eventItem)
    {
        if (_equipInventoryAllEquip != null)
        {
            switch (eventItem.TargetEquipmentInventoryName)
            {
                case "WeaponInventory":
                    BattleParameter.AttackWeapon = _equipInventoryAllEquip.WeaponInventory.Content[0];
                    break;
                case "HeadInventory":
                    BattleParameter.HeadWeapon = _equipInventoryAllEquip.HeadInventory.Content[0];
                    break;
                case "BodyInventory":
                    BattleParameter.BodyWeapon = _equipInventoryAllEquip.BodyInventory.Content[0];
                    break;
                case "FootInventory":
                    BattleParameter.FootWeapon = _equipInventoryAllEquip.FootInventory.Content[0];
                    break;
                case "AccessoryInventory":
                    BattleParameter.AccessoryWeapon = _equipInventoryAllEquip.AccessoryInventory.Content[0];
                    break;
            }
        }
    }

    /// <summary>
    /// イベントアイテムから対象インベントリを特定して装備を外す
    /// </summary>
    /// <param name="battleParameter"></param>
    public void BattleParameterUpdateUnquipments(InventoryItem eventItem)
    {
        if (_equipInventoryAllEquip != null)
        {
            switch (eventItem.TargetEquipmentInventoryName)
            {
                case "WeaponInventory":
                    BattleParameter.AttackWeapon = null;
                    break;
                case "HeadInventory":
                    BattleParameter.HeadWeapon = null;
                    break;
                case "BodyInventory":
                    BattleParameter.BodyWeapon = null;
                    break;
                case "FootInventory":
                    BattleParameter.FootWeapon = null;
                    break;
                case "AccessoryInventory":
                    BattleParameter.AccessoryWeapon = null;
                    break;
            }
        }
    }

    /// <summary>
    /// MMInventoryEventをキャッチしてそれに応じて処理を行う
    /// </summary>
    /// <param name="inventoryEvent">Inventory event.</param>
    public virtual void OnMMEvent(MMInventoryEvent inventoryEvent)
    {
        switch (inventoryEvent.InventoryEventType)
        {
            case MMInventoryEventType.InventoryOpens:
                print("Inventory Opens");
                SendBattleParameter(BattleParameter);
                break;
            case MMInventoryEventType.ItemEquipped:
                print("Inventory ItemEquipped");
                BattleParameterUpdateEquipments(inventoryEvent.EventItem);
                SendBattleParameter(BattleParameter);
                break;
            case MMInventoryEventType.ItemUnEquipped:
                print("Inventory ItemUnEquipped");
                BattleParameterUpdateUnquipments(inventoryEvent.EventItem);
                SendBattleParameter(BattleParameter);
                break;
        }
    }

    /// <summary>
    /// Enableにすると、MMInventoryEventsのリッスンを開始する。
    /// </summary>
    protected override void OnEnable()
    {
        base.OnEnable();
        this.MMEventStartListening<MMInventoryEvent>();
    }

    /// <summary>
    /// Disableの場合、MMInventoryEventsのリッスンを停止する。
    /// </summary>
    protected override void OnDisable()
    {
        base.OnDisable();
        this.MMEventStopListening<MMInventoryEvent>();
    }
}