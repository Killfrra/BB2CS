#nullable enable

using System.Numerics;

public class BBSpellScript: BBScript
{
    public int level;
    public int slot;

    [BBCall("PreLoad")] public virtual void PreLoad(){}
    [BBCall("SpellOnMissileUpdate")] public override void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition){}
    [BBCall("SpellOnMissileEnd")] public override void OnMissileEnd([BBSpellName] string spellName, Vector3 missileEndPosition){}

    // SPELL SPECIFIC
    [BBCall("CanCast", true)] public virtual bool CanCast()
    {
        return true;
    }
    [BBCall("SelfExecute")] public virtual void SelfExecute(int level){}
    [BBCall("TargetExecute")] public virtual void TargetExecute(int level, SpellMissile missileNetworkID, HitResult hitResult){}
    [BBCall("AdjustCastInfo")] public virtual void AdjustCastInfo(){}
    [BBCall("AdjustCooldown", 0)] public virtual float AdjustCooldown()
    {
        return 0;
    }
    [BBCall("ChannelingStart")] public virtual void ChannelingStart(){}
    [BBCall("ChannelingCancelStop")] public virtual void ChannelingCancelStop(){}
    [BBCall("ChannelingSuccessStop")] public virtual void ChannelingSuccessStop(){}
    [BBCall("ChannelingStop")] public virtual void ChannelingStop(){}
    [BBCall("ChannelingUpdateStats")] public virtual void ChannelingUpdateStats(){}
    [BBCall("ChannelingUpdateActions")] public virtual void ChannelingUpdateActions(){}
    [BBCall("SpellUpdateTooltip")] public virtual void UpdateTooltip(int spellSlot){}
}