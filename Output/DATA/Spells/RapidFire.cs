#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RapidFire : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon", },
            AutoBuffActivateEffect = new[]{ "rapidfire_buf.troy", },
            BuffName = "Rapid Fire",
            BuffTextureName = "Tristana_headshot.dds",
            SpellFXOverrideSkins = new[]{ "RocketTristana", },
        };
        float attackSpeedMod;
        public RapidFire(float attackSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
        }
    }
}
namespace Spells
{
    public class RapidFire : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.3f, 0.45f, 0.6f, 0.75f, 0.9f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_AttackSpeedMod;
            nextBuffVars_AttackSpeedMod = this.effect0[level];
            AddBuff(attacker, target, new Buffs.RapidFire(nextBuffVars_AttackSpeedMod), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}