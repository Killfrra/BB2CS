#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MaokaiSapling2Boom : BBSpellScript
    {
        int[] effect0 = {5, 5, 5, 5, 5};
        int[] effect1 = {40, 75, 110, 145, 180};
        int[] effect2 = {80, 130, 180, 230, 280};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 targetPos;
            TeamId teamID;
            int buffDuration; // UNUSED
            Particle particle; // UNUSED
            float damageAmount;
            int mineDamageAmount;
            Minion other1;
            float nextBuffVars_MineDamageAmount;
            bool nextBuffVars_Sprung;
            targetPos = GetUnitPosition(target);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            teamID = GetTeamID(owner);
            buffDuration = this.effect0[level];
            SpellEffectCreate(out particle, out _, "maoki_sapling_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, default, default, targetPos, true, default, default, false, false);
            damageAmount = this.effect1[level];
            mineDamageAmount = this.effect2[level];
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 240, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
            }
            other1 = SpawnMinion("DoABarrelRoll", "MaokaiSproutling", "idle.lua", targetPos, teamID, false, false, false, false, false, false, 0, false, false, (Champion)attacker);
            SetCanMove(other1, false);
            SetCanAttack(other1, false);
            nextBuffVars_MineDamageAmount = mineDamageAmount;
            nextBuffVars_Sprung = false;
            AddBuff(attacker, other1, new Buffs.MaokaiSaplingMine(nextBuffVars_MineDamageAmount, nextBuffVars_Sprung), 1, 1, 35, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}