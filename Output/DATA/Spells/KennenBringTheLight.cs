#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KennenBringTheLight : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        Particle particleID; // UNUSED
        int[] effect0 = {65, 95, 125, 155, 185};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool temp;
            temp = false;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.KennenMarkofStorm), true))
            {
                temp = true;
            }
            if(temp)
            {
                returnValue = true;
            }
            else
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 925, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.KennenMarkofStorm)) > 0)
                {
                    float baseDamage;
                    Particle hi; // UNUSED
                    BreakSpellShields(unit);
                    AddBuff(attacker, unit, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    baseDamage = this.effect0[level];
                    SpellEffectCreate(out this.particleID, out _, "kennen_btl_beam.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, attacker, "head", default, unit, "root", default, true);
                    ApplyDamage(attacker, unit, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.55f, 1, false, false, attacker);
                    SpellEffectCreate(out hi, out _, "kennen_btl_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                }
            }
        }
    }
}