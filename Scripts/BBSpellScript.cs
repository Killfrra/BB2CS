#nullable enable

public class BBSpellScript
{
    [BBCall("PreLoad")] public void PreLoad(){}
    [BBCall("SelfExecute")] public void SelfExecute(){}
    [BBCall("TargetExecute")] public void TargetExecute(AttackableUnit target, SpellMissile? missile = null){}
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
    [BBCall("SpellOnMissileEnd")] public void OnMissileEnd(SpellMissile missile){}
    [BBCall("SpellOnMissileUpdate")] public void OnMissileUpdate(SpellMissile missile){}
    [BBCall("SpellUpdateTooltip")] public void UpdateTooltip(){}
}