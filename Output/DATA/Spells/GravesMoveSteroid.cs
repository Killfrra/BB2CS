#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GravesMoveSteroid : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.4f, 0.55f, 0.7f, 0.85f, 1};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_AttackSpeedMod;
            nextBuffVars_AttackSpeedMod = this.effect0[level];
            AddBuff(attacker, target, new Buffs.GravesMoveSteroid(nextBuffVars_AttackSpeedMod), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GravesMoveSteroid : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "buffbone_cstm_barrela2", "buffbone_cstm_barrelb2", },
            AutoBuffActivateEffect = new[]{ "Graves_Move_WeaponsFX.troy", "Graves_Move_WeaponsFX.troy", },
            BuffName = "GravesSteroid",
            BuffTextureName = "GravesQuickDraw.dds",
        };
        float attackSpeedMod;
        public GravesMoveSteroid(float attackSpeedMod = default)
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