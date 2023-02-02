#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FiddlesticksDarkWind : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        Particle a; // UNUSED
        int[] effect0 = {65, 85, 105, 125, 145};
        int[] effect1 = {0, 0, 0, 0, 0};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            bool doOnce;
            float damageToDeal;
            int fiddlesticksSkinID;
            SpellBuffClear(owner, nameof(Buffs.FiddleSticksDarkWindMissile));
            teamID = GetTeamID(attacker);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            doOnce = false;
            damageToDeal = this.effect0[level];
            foreach(AttackableUnit unit in GetRandomUnitsInArea(attacker, target.Position, 600, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, 10, default, false))
            {
                if(unit != target)
                {
                    if(!doOnce)
                    {
                        bool isStealthed;
                        Vector3 attackerPos;
                        isStealthed = GetStealthed(unit);
                        if(!isStealthed)
                        {
                            attackerPos = GetUnitPosition(target);
                            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            SpellCast(attacker, unit, default, default, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, attackerPos);
                            doOnce = true;
                        }
                        else
                        {
                            bool canSee;
                            canSee = CanSeeTarget(attacker, unit);
                            if(canSee)
                            {
                                attackerPos = GetUnitPosition(target);
                                level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                                SpellCast(attacker, unit, default, default, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, attackerPos);
                                doOnce = true;
                            }
                        }
                    }
                }
            }
            AddBuff(attacker, attacker, new Buffs.FiddleSticksDarkWindMissile(), 5, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.DarkWind(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.SILENCE, 0, true, false, false);
            ApplyDamage(attacker, target, damageToDeal + this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.45f, 0, false, false, attacker);
            teamID = GetTeamID(attacker);
            fiddlesticksSkinID = GetSkinID(attacker);
            if(fiddlesticksSkinID == 6)
            {
                SpellEffectCreate(out this.a, out _, "Party_DarkWind_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.a, out _, "DarkWind_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
        }
    }
}