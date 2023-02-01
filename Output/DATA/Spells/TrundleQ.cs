#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TrundleQ : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleQ",
            BuffTextureName = "Trundle_Bite.dds",
        };
        float sapVar;
        Particle rh;
        public TrundleQ(float sapVar = default)
        {
            this.sapVar = sapVar;
        }
        public override void OnActivate()
        {
            //RequireVar(this.sapVar);
            IncFlatPhysicalDamageMod(owner, this.sapVar);
            SpellEffectCreate(out this.rh, out _, "TrundleQ_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.rh);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.sapVar);
        }
    }
}
namespace Spells
{
    public class TrundleQ : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {0.8f, 0.9f, 1, 1.1f, 1.2f};
        int[] effect1 = {30, 45, 60, 75, 90};
        int[] effect2 = {20, 25, 30, 35, 40};
        float[] effect3 = {-10, -12.5f, -15, -17.5f, -20};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float scaling;
            Vector3 attackerPos; // UNUSED
            Vector3 targetPos;
            float distance;
            float bonusDamage;
            float totalDamage;
            float scaledDamage;
            float dtD;
            int nextBuffVars_SapVar;
            float nextBuffVars_NegSapVar;
            Particle asdf; // UNUSED
            if(hitResult == HitResult.HIT_Critical)
            {
                hitResult = HitResult.HIT_Normal;
            }
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            scaling = this.effect0[level];
            attackerPos = GetUnitPosition(attacker);
            targetPos = GetUnitPosition(target);
            distance = DistanceBetweenObjects("Attacker", "Target");
            targetPos = GetPointByUnitFacingOffset(attacker, 50, 0);
            bonusDamage = this.effect1[level];
            totalDamage = GetTotalAttackDamage(attacker);
            scaledDamage = scaling * totalDamage;
            dtD = scaledDamage + bonusDamage;
            ApplyDamage(attacker, target, dtD, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            nextBuffVars_SapVar = this.effect2[level];
            nextBuffVars_NegSapVar = this.effect3[level];
            SpellEffectCreate(out asdf, out _, "globalhit_physical.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, default, default, false, false);
            AddBuff(attacker, target, new Buffs.TrundleQDebuff(nextBuffVars_SapVar, nextBuffVars_NegSapVar), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            AddBuff(attacker, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            Move(attacker, targetPos, 100, 0, 25, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 50, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            if(distance >= 75)
            {
                PlayAnimation("Spell1a", 0, attacker, false, true, true);
            }
            else
            {
                PlayAnimation("Spell1", 0, attacker, false, true, true);
            }
        }
    }
}