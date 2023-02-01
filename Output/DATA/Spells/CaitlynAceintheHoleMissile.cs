#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CaitlynAceintheHoleMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {250, 475, 700};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            Vector3 targetPos;
            float baseDamage;
            float totalDmg;
            float baseDmg;
            float bonusDmg;
            float physPreMod;
            float damageToDeal;
            Particle particle; // UNUSED
            teamID = GetTeamID(attacker);
            targetPos = GetUnitPosition(target);
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            BreakSpellShields(target);
            baseDamage = this.effect0[level];
            totalDmg = GetTotalAttackDamage(owner);
            baseDmg = GetBaseAttackDamage(owner);
            bonusDmg = totalDmg - baseDmg;
            physPreMod = 2 * bonusDmg;
            damageToDeal = physPreMod + baseDamage;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, true, true, attacker);
            SpellEffectCreate(out particle, out _, "caitlyn_ace_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, owner, default, default, true);
            SpellBuffRemove(attacker, nameof(Buffs.IfHasBuffCheck), attacker);
            DestroyMissile(missileNetworkID);
            if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.CaitlynAceintheHole)) > 0)
            {
                SpellBuffRemove(target, nameof(Buffs.CaitlynAceintheHole), attacker);
            }
            else
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, nameof(Buffs.CaitlynAceintheHole), true))
                {
                    SpellBuffRemove(unit, nameof(Buffs.CaitlynAceintheHole), attacker);
                }
            }
        }
    }
}