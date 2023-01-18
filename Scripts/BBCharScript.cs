#nullable enable

public class BBCharScript
{
    [BBCall("PreLoad")] public void PreLoad(){}
    [BBCall("CharOnActivate")] public void OnActivate(){}
    [BBCall("CharOnDeactivate")] public void OnDeactivate(){}
    [BBCall("UpdateSelfBuffActions")] public void UpdateSelfBuffActions(){}
    [BBCall("UpdateSelfBuffStats")] public void UpdateSelfBuffStats(){}
    [BBCall("CharOnAssistUnit")] public void OnAssistUnit(){}
    [BBCall("CharOnBeingHit")] public void OnBeingHit(AttackableUnit attacker){}
    [BBCall("CharOnDisconnect")] public void OnDisconnect(){}
    [BBCall("CharOnDodge")] public void OnDodge(){}
    [BBCall("CharOnHitUnit")] public void OnHitUnit(DamageData data){}
    [BBCall("CharOnKillUnit")] public void OnKillUnit(){}
    [BBCall("CharOnLaunchAttack")] public void OnLaunchAttack(Spell spell){}
    [BBCall("CharOnLevelUp")] public void OnLevelUp(){}
    [BBCall("CharOnLevelUpSpell")] public void OnLevelUpSpell(Spell spell){}
    [BBCall("CharOnMiss")] public void OnMiss(){}
    [BBCall("CharOnNearbyDeath")] public void OnNearbyDeath(){}
    [BBCall("CharOnPreAttack")] public void OnPreAttack(Spell spell){}
    [BBCall("CharOnPreDealDamage")] public void OnPreDealDamage(DamageData data){}
    [BBCall("CharOnPreDamage")] public void OnPreDamage(DamageData data){}
    [BBCall("CharOnReconnect")] public void OnReconnect(){}
    [BBCall("CharOnResurrect")] public void OnResurrect(){}
    [BBCall("CharOnSpellCast")] public void OnSpellCast(Spell spell){}
    [BBCall("SetVarsByLevel")] public void SetVarsByLevel(){}
}