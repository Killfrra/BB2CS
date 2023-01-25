#nullable enable

public class BBCharScript: BBScript
{
    public int level;
    public int talentLevel;
    
    // TALENT? SPECIFIC
    [BBCall("SetVarsByLevel")] public virtual void SetVarsByLevel(){}

    [BBCall("PreLoad")] public override void PreLoad(){}
    [BBCall("CharOnActivate")] public override void OnActivate(){}
    [BBCall("CharOnDeactivate")] public /*override*/virtual void OnDeactivate(){}
    [BBCall("UpdateSelfBuffStats")] public override void OnUpdateStats(){}
    [BBCall("UpdateSelfBuffActions")] public override void OnUpdateActions(){}
    
    [BBCall("CharOnAssistUnit")] public override void OnAssist(){}
    [BBCall("CharOnBeingHit")] public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult){}
    [BBCall("CharOnDisconnect")] public override void OnDisconnect(){}
    [BBCall("CharOnDodge")] public override void OnDodge(){}
    [BBCall("CharOnHitUnit")] public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult){}
    [BBCall("CharOnKillUnit")] public override void OnKill(){}
    [BBCall("CharOnLaunchAttack")] public override void OnLaunchAttack(){}
    [BBCall("CharOnLevelUp")] public override void OnLevelUp(){}
    [BBCall("CharOnLevelUpSpell")] public override void OnLevelUpSpell(int slot){}
    [BBCall("CharOnMiss")] public override void OnMiss(){}
    [BBCall("CharOnNearbyDeath")] public override void OnNearbyDeath(){}
    [BBCall("CharOnPreAttack")] public override void OnPreAttack(){}
    [BBCall("CharOnPreDealDamage")] public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource){}
    [BBCall("CharOnPreDamage")] public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource){}
    [BBCall("CharOnReconnect")] public override void OnReconnect(){}
    [BBCall("CharOnResurrect")] public override void OnResurrect(){}
    [BBCall("CharOnSpellCast")] public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars){}
}