#nullable enable

using System.Numerics;

public class BBSpellScript: BBScript
{
    [BBCall("PreLoad")] public void PreLoad(){}
    [BBCall("SpellOnMissileUpdate")] public override void OnMissileUpdate(Vector3 missilePosition){}
    [BBCall("SpellOnMissileEnd")] public override void OnMissileEnd(string spellName, Vector3 missileEndPosition){}

    // SPELL SPECIFIC
    [BBCall("SelfExecute")] public void SelfExecute(int level){}
    [BBCall("TargetExecute")] public void TargetExecute(int level, SpellMissile missileNetworkID, HitResult hitResult){}
    [BBCall("AdjustCastInfo")] public void AdjustCastInfo(){}
    [BBCall("AdjustCooldown")] public void AdjustCooldown(){}
    [BBCall("CanCast")] public bool CanCast()
    {
        return true;
    }
    [BBCall("ChannelingStart")] public void ChannelingStart(){}
    [BBCall("ChannelingCancelStop")] public void ChannelingCancelStop(){}
    [BBCall("ChannelingSuccessStop")] public void ChannelingSuccessStop(){}
    [BBCall("ChannelingStop")] public void ChannelingStop(){}
    [BBCall("ChannelingUpdateStats")] public void ChannelingUpdateStats(){}
    [BBCall("ChannelingUpdateActions")] public void ChannelingUpdateActions(){}

    [BBCall("SpellUpdateTooltip")] public void UpdateTooltip(int spellSlot){}
}