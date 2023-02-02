#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VladimirHemoplagueMissile : BBSpellScript
    {
        int[] effect0 = {150, 250, 350};
        float[] effect1 = {-0.14f, -0.14f, -0.14f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_DamagePerLevel;
            TeamId teamofOwner;
            int vladSkinID;
            Particle particle; // UNUSED
            object targetPos; // UNITIALIZED
            float nextBuffVars_DamageIncrease;
            object nextBuffVars_TargetPos; // UNUSED
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell4", 0.5f, owner, false, true, true);
            teamofOwner = GetTeamID(owner);
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            vladSkinID = GetSkinID(owner);
            if(vladSkinID == 5)
            {
                if(teamofOwner == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out particle, out _, "VladHemoplague_BloodKing_nova.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position, owner, default, target.Position, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out particle, out _, "VladHemoplague_BloodKing_nova.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position, owner, default, target.Position, true, false, false, false, false);
                }
            }
            else
            {
                if(teamofOwner == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out particle, out _, "VladHemoplague_nova.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position, owner, default, target.Position, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out particle, out _, "VladHemoplague_nova.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position, owner, default, target.Position, true, false, false, false, false);
                }
            }
            nextBuffVars_DamagePerLevel = this.effect0[level];
            nextBuffVars_DamageIncrease = this.effect1[level];
            nextBuffVars_TargetPos = targetPos;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.VladimirHemoplagueDebuff(nextBuffVars_DamageIncrease, nextBuffVars_DamagePerLevel), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            }
        }
    }
}