#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MordekaiserNukeOfTheBeast : BBSpellScript
    {
        int[] effect0 = {80, 110, 140, 170, 200};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            Particle aasdf; // UNUSED
            float baseDamage;
            float baseDamage;
            float totalDamage;
            float bonusDamage;
            float abilityPower;
            float bonusAPDamage;
            float nextBuffVars_BaseDamage;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out aasdf, out _, "mordakaiser_maceOfSpades_tar2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = this.effect0[level];
            baseDamage = GetBaseAttackDamage(owner);
            totalDamage = GetTotalAttackDamage(owner);
            bonusDamage = totalDamage - baseDamage;
            baseDamage += bonusDamage;
            abilityPower = GetFlatMagicDamageMod(owner);
            bonusAPDamage = abilityPower * 0.4f;
            baseDamage += bonusAPDamage;
            nextBuffVars_BaseDamage = baseDamage;
            AddBuff((ObjAIBase)target, owner, new Buffs.MordekaiserNukeOfTheBeastDmg(nextBuffVars_BaseDamage), 5, 1, 0.001f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}