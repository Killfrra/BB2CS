#nullable enable

public class BBItemScript: BBScript
{
    public int slot;

    // ITEM SPECIFIC
    [BBCall("UpdateAura")] public virtual void UpdateAura(){} //TODO: Verify
    
    [BBCall("PreLoad")] public override void PreLoad(){}
    [BBCall("OnActivate")] public override void OnActivate(){}
    [BBCall("OnDeactivate")] public /*override*/virtual void OnDeactivate(){}
    [BBCall("UpdateSelfBuffStats")] public override void OnUpdateStats(){}
    [BBCall("UpdateSelfBuffActions")] public override void OnUpdateActions(){}
    [BBCall("ItemOnAssist")] public override void OnAssist(){}
    [BBCall("ItemOnBeingDodged")] public override void OnBeingDodged(){}
    [BBCall("ItemOnDeath")] public override void OnDeath(){}
    [BBCall("ItemOnHitUnit")] public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult){}
    [BBCall("ItemOnBeingHit")] public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult){}
    [BBCall("ItemOnKill")] public override void OnKill(){}
    [BBCall("ItemOnMiss")] public override void OnMiss(){}
    [BBCall("ItemOnPreDamage")] public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource){}
    [BBCall("ItemOnPreDealDamage")] public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource){}
    [BBCall("ItemOnDealDamage")] public override void OnDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource){}
    [BBCall("ItemOnSpellCast")] public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars){}
}