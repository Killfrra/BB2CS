#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GGun : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GatlingGunSelf",
            BuffTextureName = "Corki_GatlingGun.dds",
        };
        float lastTimeExecuted;
        Particle gatlingEffect; // UNUSED
        public override void OnUpdateActions()
        {
            Vector3 pos;
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                SpellEffectCreate(out this.gatlingEffect, out _, "corki_gatlin_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                pos = GetPointByUnitFacingOffset(owner, 300, 0);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            }
        }
    }
}
namespace Spells
{
    public class GGun : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 20f, 16f, 12f, 8f, 4f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {4, 4, 4, 4, 4};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff(attacker, target, new Buffs.GGun(), 1, 1, this.effect0[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}