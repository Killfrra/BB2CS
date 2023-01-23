using System.Numerics;

public class BBScript
{
    public virtual void PreLoad(){}
    
    public virtual void OnActivate(){}
    public virtual void OnDeactivate(){}
    
    // UPDATE
    public virtual void OnUpdateStats(){}
    public virtual void OnUpdateActions(){}

    public virtual void OnDodge(){}
    public virtual void OnBeingDodged(){}

    // HIT
    public virtual void OnHitUnit(float damageAmount, DamageType damageType, HitResult hitResult){}
    public virtual void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult){}
    public virtual void OnSpellHit(){}
    public virtual void OnBeingSpellHit(SpellScriptMetaData spellVars){}
    public virtual void OnMiss(){}

    public virtual void OnMoveEnd(){}
    public virtual void OnMoveFailure(){}
    public virtual void OnMoveSuccess(){}
    public virtual void OnCollision(){}
    public virtual void OnCollisionTerrain(){}

    // DEATH
    public virtual void OnKill(){}
    public virtual void OnAssist(){}
    public virtual void OnDeath(){}
    public virtual void OnNearbyDeath(){}
    public virtual void OnZombie(){}
    public virtual void OnResurrect(){}

    // CONNECTION
    public virtual void OnDisconnect(){}
    public virtual void OnReconnect(){}

    // LEVEL
    public virtual void OnLevelUp(){}
    public virtual void OnLevelUpSpell(int slot){}
    
    public virtual void OnPreAttack(){}
    public virtual void OnLaunchAttack(){}
    public virtual void OnLaunchMissile(SpellMissile missileId){}
    public virtual void OnMissileUpdate(SpellMissile missileNetworkId, Vector3 missilePosition){}
    public virtual void OnMissileEnd(string spellName, Vector3 missileEndPosition){}

    // DAMAGE
    public virtual void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource){}
    public virtual void OnPreMitigationDamage(){}
    public virtual void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource){}
    public virtual void OnTakeDamage(float damageAmount){}
    public virtual void OnDealDamage(){}

    public virtual void OnHeal(float health){}
    
    public virtual void OnSpellCast(string spellName, SpellScriptMetaData spellVars){}
}