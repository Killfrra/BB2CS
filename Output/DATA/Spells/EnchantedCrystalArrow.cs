#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EnchantedCrystalArrow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "head", },
            AutoBuffActivateEffect = new[]{ "", "Stun_glb.troy", },
            BuffName = "Enchanted Crystal Arrow",
            BuffTextureName = "Bowmaster_EnchantedArrow.dds",
            PopupMessage = new[]{ "game_floatingtext_Stunned", },
        };
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}
namespace Spells
{
    public class EnchantedCrystalArrow : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {250, 425, 600};
        float[] effect1 = {-0.5f, -0.5f, -0.5f};
        int[] effect2 = {0, 0, 0};
        int[] effect3 = {3, 3, 3};
        int[] effect4 = {125, 212, 300};
        float[] effect5 = {-0.5f, -0.5f, -0.5f};
        int[] effect6 = {0, 0, 0};
        int[] effect7 = {3, 3, 3};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float distance;
            float stunDuration;
            float nextBuffVars_MovementSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            distance = DistanceBetweenObjectAndPoint(target, charVars.CastPoint);
            stunDuration = distance * 0.00125f;
            stunDuration = Math.Max(stunDuration, 1);
            stunDuration = Math.Min(stunDuration, 3.5f);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(unit == target)
                {
                    ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
                    AddBuff(attacker, unit, new Buffs.EnchantedCrystalArrow(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    nextBuffVars_MovementSpeedMod = this.effect1[level];
                    nextBuffVars_AttackSpeedMod = this.effect2[level];
                    AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, this.effect3[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
                else
                {
                    BreakSpellShields(target);
                    ApplyDamage(attacker, unit, this.effect4[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
                    nextBuffVars_MovementSpeedMod = this.effect5[level];
                    nextBuffVars_AttackSpeedMod = this.effect6[level];
                    AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, this.effect7[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
            DestroyMissile(missileNetworkID);
        }
    }
}