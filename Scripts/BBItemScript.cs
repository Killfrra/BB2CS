#nullable enable

public class BBItemScript
{
    [BBCall("PreLoad")] public void PreLoad(){}
    [BBCall("OnActivate")] public void OnActivate(ObjAIBase Owner){}
    [BBCall("OnDeactivate")] public void OnDeactivate(ObjAIBase owner){}
    [BBCall("UpdateSelfBuffActions")] public void UpdateSelfBuffActions(){}
    [BBCall("UpdateSelfBuffStats")] public void UpdateSelfBuffStats(){}
    [BBCall("ItemOnAssist")] public void OnAssist(){}
    [BBCall("ItemOnBeingDodged")] public void OnBeingDodged(){}
    [BBCall("ItemOnBeingHit")] public void OnBeingHit(AttackableUnit attacker){}
    [BBCall("ItemOnDeath")] public void OnDeath(DeathData data){}
    [BBCall("ItemOnHitUnit")] public void OnHitUnit(DamageData data){}
    [BBCall("ItemOnKill")] public void OnKill(DeathData data){}
    [BBCall("ItemOnMiss")] public void OnMiss(){}
    [BBCall("ItemOnPreDamage")] public void OnPreDamage(DamageData data){}
    [BBCall("ItemOnPreDealDamage")] public void OnPreDealDamage(DamageData data){}
    [BBCall("ItemOnDealDamage")] public void OnDealDamage(DamageData data){}
    [BBCall("ItemOnSpellCast")] public void OnSpellCast(Spell spell){}
}