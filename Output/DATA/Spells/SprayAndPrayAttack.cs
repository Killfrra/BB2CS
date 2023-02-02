#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SprayAndPrayAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId twitchTeamId;
            float baseDamage;
            int twitchSkinID;
            twitchTeamId = GetTeamID(owner);
            baseDamage = GetBaseAttackDamage(owner);
            twitchSkinID = GetSkinID(attacker);
            if(target is ObjAIBase)
            {
                Particle a; // UNUSED
                if(twitchSkinID == 4)
                {
                    SpellEffectCreate(out a, out _, "twitch_gangster_sprayandPray_tar.troy", default, twitchTeamId ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                }
                else if(twitchSkinID == 5)
                {
                    SpellEffectCreate(out a, out _, "twitch_punk_sprayandPray_tar.troy", default, twitchTeamId ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "twitch_sprayandPray_tar.troy", default, twitchTeamId ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                }
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, (ObjAIBase)owner);
        }
    }
}