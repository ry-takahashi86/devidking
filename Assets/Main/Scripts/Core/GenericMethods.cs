using MoreMountains.Tools;
using UnityEngine;

public static class GenericMethods
{
    /// <summary>
    /// ダメージ計算メソッド
    /// </summary>
    /// <param name="minDamage"></param>
    /// <param name="maxDamage"></param>
    /// <param name="battleParameter"></param>
    /// <returns></returns>
    public static int CalculateDamage(float minDamage, float maxDamage, GameObject owner, GameObject target)
    {
        // 関係者のパラメータを取得する
        CharacterBattleParameter _owner = owner.MMGetComponentNoAlloc<CharacterBattleParameter>();
        CharacterBattleParameter _target = target.gameObject.MMGetComponentNoAlloc<CharacterBattleParameter>();

        // バトルパラメーターがない場合は、ベースダメージでランダムに返す
        if (_owner == null || _target == null)
        {
            return (int)UnityEngine.Random.Range(minDamage, Mathf.Max(maxDamage, minDamage));
        }

        // バトルパラメーターがある場合は、ダメージ倍率とダメージ減少率を考慮してダメージを計算する
        float _minDamage = minDamage * _owner.BattleParameter.AttackPower;
        _minDamage = _minDamage - (_minDamage * _target.BattleParameter.DefensePower / 100);
        float _maxDamage = maxDamage * _owner.BattleParameter.AttackPower;
        _maxDamage = _maxDamage - (_maxDamage * _target.BattleParameter.DefensePower / 100);
        return (int)UnityEngine.Random.Range(_minDamage, Mathf.Max(_minDamage, _maxDamage));
    }
}