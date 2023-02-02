#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Expunge : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {20, 30, 40, 50, 60};
        int[] effect1 = {30, 60, 90, 120, 150};
        public override void SelfExecute()
        {
            TeamId teamID;
            float explosionDamage;
            teamID = GetTeamID(owner);
            explosionDamage = this.effect0[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.DeadlyVenom)) > 0)
                {
                    int count;
                    float baseDamage;
                    float bonusDamage;
                    float totalDamage;
                    Particle asdf; // UNUSED
                    BreakSpellShields(unit);
                    count = GetBuffCountFromAll(unit, nameof(Buffs.DeadlyVenom));
                    baseDamage = this.effect1[level];
                    bonusDamage = count * explosionDamage;
                    totalDamage = baseDamage + bonusDamage;
                    ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
                    SpellEffectCreate(out asdf, out _, "Expunge_tar_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, true);
                    SpellBuffRemoveStacks(unit, owner, nameof(Buffs.DeadlyVenom), 0);
                }
            }
        }
    }
}