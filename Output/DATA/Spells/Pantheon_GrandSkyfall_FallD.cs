#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pantheon_GrandSkyfall_FallD : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pantheon_GrandSkyfall_FallDamage",
        };
        float damageRank;
        int[] effect0 = {400, 700, 1000};
        public override void OnDeactivate(bool expired)
        {
            Vector3 targetPos;
            int level;
            Vector3 unitPos;
            float distance;
            float percentDamage;
            float percentNotDamage;
            float nextBuffVars_MoveSpeedMod;
            Particle a; // UNUSED
            targetPos = charVars.TargetPos;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.damageRank = this.effect0[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)target, targetPos, 700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                unitPos = GetUnitPosition(unit);
                distance = DistanceBetweenPoints(targetPos, unitPos);
                if(distance <= 250)
                {
                    percentDamage = 1;
                }
                else
                {
                    percentNotDamage = distance - 200;
                    percentNotDamage = distance / 500;
                    percentDamage = 1 - percentNotDamage;
                    percentDamage = Math.Min(percentDamage, 1);
                    percentDamage = Math.Max(percentDamage, 0.5f);
                }
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.damageRank, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentDamage, 1, 1, false, false, attacker);
                nextBuffVars_MoveSpeedMod = -0.35f;
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                SpellEffectCreate(out a, out _, "Globalhit_physical.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "head", default, target, default, default, false, default, default, false, false);
            }
        }
    }
}