#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KennenShurikenHurlMissile1 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {75, 115, 155, 195, 235};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float properDamage;
            bool isStealthed;
            Particle gfasdf; // UNUSED
            bool canSee;
            teamID = GetTeamID(owner);
            properDamage = this.effect0[level];
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                ApplyDamage(attacker, target, properDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.75f, 1, false, false, attacker);
                SpellEffectCreate(out gfasdf, out _, "Kennen_ts_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                DestroyMissile(missileNetworkID);
            }
            else
            {
                if(target is Champion)
                {
                    BreakSpellShields(target);
                    AddBuff(attacker, target, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    ApplyDamage(attacker, target, properDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.75f, 1, false, false, attacker);
                    SpellEffectCreate(out gfasdf, out _, "Kennen_ts_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        BreakSpellShields(target);
                        AddBuff(attacker, target, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        ApplyDamage(attacker, target, properDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.75f, 1, false, false, attacker);
                        SpellEffectCreate(out gfasdf, out _, "Kennen_ts_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                        DestroyMissile(missileNetworkID);
                    }
                }
            }
        }
    }
}