#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KogMawVoidOozeMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {-0.28f, -0.36f, -0.44f, -0.52f, -0.6f};
        int[] effect1 = {60, 110, 160, 210, 260};
        public override void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition)
        {
            float nextBuffVars_SlowPercent;
            Vector3 groundHeight;
            Vector3 nextBuffVars_targetPos;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_SlowPercent = this.effect0[level];
            groundHeight = GetGroundHeight(missilePosition);
            groundHeight = ModifyPosition(0, 10, 0);
            nextBuffVars_targetPos = groundHeight;
            AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawVoidOozeMissile(nextBuffVars_SlowPercent, nextBuffVars_targetPos), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, false, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(owner.Team != target.Team)
            {
                Particle varrr; // UNUSED
                TeamId casterID2; // UNITIALIZED
                ApplyDamage(attacker, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
                SpellEffectCreate(out varrr, out _, "KogMawVoidOoze_tar.troy", default, casterID2 ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class KogMawVoidOozeMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "KogMaw_VoidOoze.dds",
        };
        float slowPercent;
        Vector3 targetPos;
        Particle particle;
        Particle particle2;
        float lastTimeExecuted;
        public KogMawVoidOozeMissile(float slowPercent = default, Vector3 targetPos = default)
        {
            this.slowPercent = slowPercent;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            //RequireVar(this.slowPercent);
            //RequireVar(this.targetPos);
            teamOfOwner = GetTeamID(owner);
            targetPos = this.targetPos;
            SpellEffectCreate(out this.particle, out this.particle2, "KogMawVoidOoze_green.troy", "KogMawVoidOoze_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 240, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, default, default, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos;
            targetPos = this.targetPos;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_SlowPercent;
                    nextBuffVars_SlowPercent = this.slowPercent;
                    AddBuff(attacker, unit, new Buffs.KogMawVoidOozeSlow(nextBuffVars_SlowPercent), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}