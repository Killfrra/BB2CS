#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GragasExplosiveCaskBoom : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
        };
        int[] effect0 = {200, 325, 450};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamofOwner;
            int gragasSkinID;
            Particle particle; // UNUSED
            Vector3 center;
            int nextBuffVars_Speed;
            int nextBuffVars_Gravity;
            Vector3 nextBuffVars_Center;
            int nextBuffVars_Distance;
            int nextBuffVars_IdealDistance;
            Particle arr; // UNUSED
            teamofOwner = GetTeamID(owner);
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            gragasSkinID = GetSkinID(attacker);
            if(gragasSkinID == 4)
            {
                if(teamofOwner == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out particle, out _, "gragas_caskboom_classy.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position, owner, default, target.Position, true, default, default, false);
                }
                else
                {
                    SpellEffectCreate(out particle, out _, "gragas_caskboom_classy.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position, owner, default, target.Position, true, default, default, false);
                }
            }
            else
            {
                if(teamofOwner == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out particle, out _, "gragas_caskboom.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position, owner, default, target.Position, true, default, default, false);
                }
                else
                {
                    SpellEffectCreate(out particle, out _, "gragas_caskboom.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position, owner, default, target.Position, true, default, default, false);
                }
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 430, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                center = GetCastSpellTargetPos();
                nextBuffVars_Speed = 900;
                nextBuffVars_Gravity = 5;
                nextBuffVars_Center = center;
                nextBuffVars_Distance = 900;
                nextBuffVars_IdealDistance = 900;
                ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.MoveAwayCollision(nextBuffVars_Speed, nextBuffVars_Gravity, nextBuffVars_Center, nextBuffVars_Distance), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                if(teamofOwner == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out arr, out _, "gragas_caskwine_tar.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, unit.Position, unit, default, default, true, default, default, false);
                }
                else
                {
                    SpellEffectCreate(out arr, out _, "gragas_caskwine_tar.troy", default, TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, unit.Position, unit, default, default, true, default, default, false);
                }
            }
        }
    }
}