#nullable enable

using System.Numerics;

public class BBBuffScript: BBScript, IHandleAttackableUnitEvents
{
    // BUFF SPECIFIC
    [BBCall("UpdateBuffs")] public void UpdateBuffs(){}
    [BBCall("BuffOnUpdateAmmo")] public void OnUpdateAmmo(){}
    [BBCall("BuffOnAllowAdd")] public bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
    {
        return true;
    }

    [BBCall("PreLoad")] public override void PreLoad(){}
    [BBCall("OnBuffActivate")] public override void OnActivate(){}
    [BBCall("OnBuffDeactivate")] public override void OnDeactivate(/*bool expired*/){}
    [BBCall("BuffOnUpdateStats")] public override void OnUpdateStats(){}
    [BBCall("BuffOnUpdateActions")] public override void OnUpdateActions(){}

    [BBCall("BuffOnDeath")] public override void OnDeath(){}
    [BBCall("BuffOnAssist")] public override void OnAssist(){}
    [BBCall("BuffBeingDodged")] public override void OnBeingDodged(){}
    [BBCall("BuffOnBeingHit")] public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult){}
    [BBCall("BuffOnBeingSpellHit")] public override void OnBeingSpellHit(SpellScriptMetaData spellVars){}
    [BBCall("BuffOnCollision")] public override void OnCollision(){}
    [BBCall("BuffOnCollisionTerrain")] public override void OnCollisionTerrain(){}
    [BBCall("BuffOnDisconnect")] public override void OnDisconnect(){}
    [BBCall("BuffOnHeal")] public override void OnHeal(float health){}
    [BBCall("BuffOnHitUnit")] public override void OnHitUnit(float damageAmount, DamageType damageType, HitResult hitResult){}
    [BBCall("BuffOnKill")] public override void OnKill(){}
    [BBCall("BuffOnLaunchAttack")] public override void OnLaunchAttack(){}
    [BBCall("BuffOnLaunchMissile")] public override void OnLaunchMissile(SpellMissile missileId){}
    [BBCall("BuffOnLevelUp")] public override void OnLevelUp(){}
    [BBCall("BuffOnLevelUpSpell")] public override void OnLevelUpSpell(int slot){}
    [BBCall("BuffOnMiss")] public override void OnMiss(){}
    [BBCall("BuffOnMissileEnd")] public override void OnMissileEnd(string spellName, Vector3 missileEndPosition){}
    [BBCall("BuffOnMoveEnd")] public override void OnMoveEnd(){}
    [BBCall("BuffOnMoveFailure")] public override void OnMoveFailure(){}
    [BBCall("BuffOnMoveSuccess")] public override void OnMoveSuccess(){}
    [BBCall("BuffOnPreAttack")] public override void OnPreAttack(){}
    [BBCall("BuffOnReconnect")] public override void OnReconnect(){}
    [BBCall("BuffOnResurrect")] public override void OnResurrect(){}
    [BBCall("BuffOnSpellCast")] public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars){}
    [BBCall("BuffOnSpellHit")] public override void OnSpellHit(){}
    [BBCall("BuffOnPreDealDamage")] public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource){}
    [BBCall("BuffOnPreMitigationDamage")] public override void OnPreMitigationDamage(){}
    [BBCall("BuffOnPreDamage")] public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource){}
    [BBCall("BuffOnTakeDamage")] public override void OnTakeDamage(float damageAmount){}
    [BBCall("BuffOnDealDamage")] public override void OnDealDamage(){}
    [BBCall("BuffOnZombie")] public override void OnZombie(){}
    [BBCall("BuffOnDodge")] public override void OnDodge(){}
}