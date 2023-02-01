#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaDissonanceWave : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "KogMaw_VoidOoze.dds",
        };
        Particle particle2;
        Particle particle;
        Vector3 targetPos;
        float lastTimeExecuted;
        public OrianaDissonanceWave(Particle particle2 = default, Particle particle = default, Vector3 targetPos = default)
        {
            this.particle2 = particle2;
            this.particle = particle;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner; // UNUSED
            Vector3 targetPos; // UNUSED
            //RequireVar(this.targetPos);
            //RequireVar(this.particle);
            //RequireVar(this.particle2);
            teamOfOwner = GetTeamID(owner);
            targetPos = this.targetPos;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos;
            int nextBuffVars_Level;
            targetPos = this.targetPos;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.OrianaShock), false))
                {
                    nextBuffVars_Level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    AddBuff(attacker, unit, new Buffs.OrianaSlow(nextBuffVars_Level), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 225, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.OrianaShock), false))
                {
                    nextBuffVars_Level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    AddBuff(attacker, unit, new Buffs.OrianaHaste(nextBuffVars_Level), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}