#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OrianaDissonanceCommand : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {70, 115, 160, 205, 250};
        public override void SelfExecute()
        {
            int nextBuffVars_Level;
            Particle nextBuffVars_Particle2;
            Particle nextBuffVars_Particle;
            Vector3 nextBuffVars_targetPos;
            TeamId teamID;
            float damage;
            bool deployed;
            float selfAP;
            float bonusDamage;
            Vector3 targetPos;
            Particle particle;
            Particle particle2;
            Particle temp; // UNUSED
            PlayAnimation("Spell2", 0, owner, false, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            teamID = GetTeamID(owner);
            damage = this.effect0[level];
            deployed = false;
            selfAP = GetFlatMagicDamageMod(owner);
            bonusDamage = selfAP * 0.5f;
            damage += bonusDamage;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUntargetable, 1, nameof(Buffs.OrianaGhost), true))
            {
                deployed = true;
                targetPos = GetUnitPosition(unit);
                if(unit is Champion)
                {
                    SpellEffectCreate(out particle, out particle2, "OrianaDissonance_ally_green.troy", "OrianaDissonance_ally_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out particle, out particle2, "OrianaDissonance_ball_green.troy", "OrianaDissonance_ball_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
                }
            }
            if(!deployed)
            {
                targetPos = GetUnitPosition(owner);
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.OriannaBallTracker)) > 0)
                {
                    targetPos = charVars.BallPosition;
                }
                SpellEffectCreate(out particle, out particle2, "OrianaDissonance_cas_green.troy", "OrianaDissonance_cas_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, targetPos, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                SpellEffectCreate(out temp, out _, "OrianaDissonance_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                ApplyDamage((ObjAIBase)owner, unit, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, (ObjAIBase)owner);
                nextBuffVars_Level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                AddBuff(attacker, unit, new Buffs.OrianaSlow(nextBuffVars_Level), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, targetPos, 225, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                nextBuffVars_Level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                AddBuff(attacker, unit, new Buffs.OrianaHaste(nextBuffVars_Level), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            nextBuffVars_Particle2 = particle2;
            nextBuffVars_Particle = particle;
            nextBuffVars_targetPos = targetPos;
            AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaDissonanceWave(nextBuffVars_Particle2, nextBuffVars_Particle, nextBuffVars_targetPos), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}