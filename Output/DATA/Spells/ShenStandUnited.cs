#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShenStandUnited : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Shen Stand United Channel",
            BuffTextureName = "Shen_StandUnited.dds",
        };
        Particle particleID;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.particleID, out _, "ShenTeleport_v2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleID);
        }
    }
}
namespace Spells
{
    public class ShenStandUnited : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 2.5f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {200, 475, 750};
        public override void ChannelingStart()
        {
            float nextBuffVars_shieldHealth;
            float baseShieldHealth;
            float abilityPower;
            float bonusShieldHealth;
            float shieldHealth;
            AddBuff(attacker, owner, new Buffs.ShenStandUnited(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            baseShieldHealth = this.effect0[level];
            abilityPower = GetFlatMagicDamageMod(owner);
            bonusShieldHealth = 1.5f * abilityPower;
            shieldHealth = baseShieldHealth + bonusShieldHealth;
            nextBuffVars_shieldHealth = shieldHealth;
            AddBuff((ObjAIBase)owner, target, new Buffs.ShenStandUnitedShield(nextBuffVars_shieldHealth), 1, 1, 7.5f, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.ShenStandUnitedTarget(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            Vector3 castPos;
            DestroyMissileForTarget(owner);
            castPos = GetPointByUnitFacingOffset(target, 150, 180);
            TeleportToPosition(owner, castPos);
        }
    }
}