#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class IceBlast : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {-0.2f, -0.3f, -0.4f, -0.5f, -0.6f};
        int[] effect1 = {85, 130, 175, 225, 275};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MovementSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            nextBuffVars_MovementSpeedMod = this.effect0[level];
            nextBuffVars_AttackSpeedMod = -0.25f;
            ApplyDamage(attacker, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.IceBlast(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class IceBlast : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Freeze.troy", },
            BuffName = "Iceblast",
            BuffTextureName = "Yeti_IceBlast.dds",
        };
        float movementSpeedMod;
        float attackSpeedMod;
        public IceBlast(float movementSpeedMod = default, float attackSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeedMod);
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.movementSpeedMod);
            IncPercentMultiplicativeAttackSpeedMod(owner, this.attackSpeedMod);
        }
    }
}