#nullable enable

public class BBBuffScript
{
    [BBCall("PreLoad")] public void PreLoad(){}
    [BBCall("OnBuffActivate")] public void OnActivate(){}
    [BBCall("OnBuffDeactivate")] public void OnDeactivate(){}
    [BBCall("BuffOnDeath")] public void OnDeath(DeathData data){}
    [BBCall("UpdateBuffs")] public void UpdateBuffs(){}
    [BBCall("BuffOnUpdateStats")] public void OnUpdateStats(){}
    [BBCall("BuffOnUpdateActions")] public void OnUpdateActions(){}
    [BBCall("BuffOnAllowAdd")] public bool OnAllowAdd(Buff buff)
    {
        return true;
    }
    [BBCall("BuffOnAssist")] public void OnAssist(){}
    [BBCall("BuffBeingDodged")] public void OnBeingDodged(){}
    [BBCall("BuffOnBeingHit")] public void OnBeingHit(AttackableUnit attacker){}
    [BBCall("BuffOnBeingSpellHit")] public void OnBeingSpellHit(Spell spell, SpellMissile missile){}
    [BBCall("BuffOnCollision")] public void OnCollision(){}
    [BBCall("BuffOnCollisionTerrain")] public void OnCollisionTerrain(){}
    [BBCall("BuffOnDisconnect")] public void OnDisconnect(){}
    [BBCall("BuffOnHeal")] public void OnHeal(){}
    [BBCall("BuffOnHitUnit")] public void OnHitUnit(DamageData data){}
    [BBCall("BuffOnKill")] public void OnKill(DeathData data){}
    [BBCall("BuffOnLaunchAttack")] public void OnLaunchAttack(Spell spell){}
    [BBCall("BuffOnLaunchMissile")] public void OnLaunchMissile(Spell spell, SpellMissile missile){}
    [BBCall("BuffOnLevelUp")] public void OnLevelUp(){}
    [BBCall("BuffOnLevelUpSpell")] public void OnLevelUpSpell(Spell spell){}
    [BBCall("BuffOnMiss")] public void OnMiss(){}
    [BBCall("BuffOnMissileEnd")] public void OnMissileEnd(){}
    [BBCall("BuffOnMoveEnd")] public void OnMoveEnd(){}
    [BBCall("BuffOnMoveFailure")] public void OnMoveFailure(){}
    [BBCall("BuffOnMoveSuccess")] public void OnMoveSuccess(){}
    [BBCall("BuffOnPreAttack")] public void OnPreAttack(Spell spell){}
    [BBCall("BuffOnReconnect")] public void OnReconnect(){}
    [BBCall("BuffOnResurrect")] public void OnResurrect(){}
    [BBCall("BuffOnSpellCast")] public void OnSpellCast(Spell spell){}
    [BBCall("BuffOnSpellHit")] public void OnSpellHit(Spell spell, AttackableUnit target, SpellMissile missile){}
    [BBCall("BuffOnPreDealDamage")] public void OnPreDealDamage(DamageData data){}
    [BBCall("BuffOnPreMitigationDamage")] public void OnPreMitigationDamage(DamageData data){}
    [BBCall("BuffOnPreDamage")] public void OnPreDamage(DamageData data){}
    [BBCall("BuffOnTakeDamage")] public void OnTakeDamage(DamageData data){}
    [BBCall("BuffOnDealDamage")] public void OnDealDamage(DamageData data){}
    [BBCall("BuffOnUpdateAmmo")] public void OnUpdateAmmo(){}
    [BBCall("BuffOnZombie")] public void OnZombie(){}
}