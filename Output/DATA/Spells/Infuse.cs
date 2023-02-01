#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Infuse : BBBuffScript
    {
    }
}
namespace Spells
{
    public class Infuse : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {25, 50, 75, 100, 125};
        int[] effect1 = {25, 50, 75, 100, 125};
        int[] effect2 = {50, 100, 150, 200, 250};
        float[] effect3 = {1.5f, 1.75f, 2, 2.25f, 2.5f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            Particle infuse; // UNUSED
            teamID = GetTeamID(attacker);
            if(target.Team == owner.Team)
            {
                ApplyAssistMarker((ObjAIBase)owner, target, 10);
                if(target != owner)
                {
                    IncPAR(target, this.effect0[level], PrimaryAbilityResourceType.MANA);
                }
                IncPAR(owner, this.effect1[level], PrimaryAbilityResourceType.MANA);
                SpellEffectCreate(out infuse, out _, "soraka_infuse_ally_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                if(target != owner)
                {
                    SpellEffectCreate(out infuse, out _, "soraka_infuse_ally_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                }
            }
            else
            {
                ApplyDamage(attacker, target, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.75f, 1, false, false, attacker);
                ApplySilence(attacker, target, this.effect3[level]);
                SpellEffectCreate(out infuse, out _, "soraka_infuse_enemy_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
        }
    }
}