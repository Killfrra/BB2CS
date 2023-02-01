#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Wish : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {200, 320, 440};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float spellPower;
            float baseHealthToHeal;
            float spellPowerBonus;
            float healthToHeal;
            float temp1;
            Particle infuse; // UNUSED
            teamID = GetTeamID(attacker);
            spellPower = GetFlatMagicDamageMod(owner);
            baseHealthToHeal = this.effect0[level];
            spellPowerBonus = spellPower * 0.7f;
            healthToHeal = baseHealthToHeal + spellPowerBonus;
            IncHealth(target, healthToHeal, owner);
            temp1 = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
            if(temp1 < 1)
            {
                SpellEffectCreate(out infuse, out _, "Wish_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                ApplyAssistMarker((ObjAIBase)owner, target, 10);
            }
        }
    }
}