﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SejuaniFrostApply : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 12f, 11f, 10f, 9f, 8f, },
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MovementSpeedMod;
            float frostDuration;
            TeamId teamID;
            nextBuffVars_MovementSpeedMod = -0.1f;
            frostDuration = 3;
            teamID = GetTeamID(owner);
            if(target is Champion)
            {
                AddBuff(attacker, target, new Buffs.SejuaniFrostApplyParticle(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if(teamID == TeamId.TEAM_BLUE)
                {
                    if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniWintersClaw)) > 0)
                    {
                        AddBuff(attacker, target, new Buffs.SejuaniFrostTracker(), 1, 1, frostDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, target, new Buffs.SejuaniFrost(nextBuffVars_MovementSpeedMod), 1, 1, frostDuration, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    }
                }
                else
                {
                    if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniWintersClawChaos)) > 0)
                    {
                        AddBuff(attacker, target, new Buffs.SejuaniFrostTracker(), 1, 1, frostDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, target, new Buffs.SejuaniFrostChaos(nextBuffVars_MovementSpeedMod), 1, 1, frostDuration, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    }
                }
            }
            else if(default is not BaseTurret)
            {
                AddBuff(attacker, target, new Buffs.SejuaniFrostApplyPartMinion(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if(teamID == TeamId.TEAM_BLUE)
                {
                    if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniWintersClaw)) > 0)
                    {
                        AddBuff(attacker, target, new Buffs.SejuaniFrostTracker(), 1, 1, frostDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, target, new Buffs.SejuaniFrost(nextBuffVars_MovementSpeedMod), 1, 1, frostDuration, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                        if(GetBuffCountFromCaster(target, target, nameof(Buffs.ResistantSkin)) > 0)
                        {
                            AddBuff(attacker, target, new Buffs.SejuaniFrostResist(), 1, 1, frostDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        }
                        if(GetBuffCountFromCaster(target, target, nameof(Buffs.ResistantSkinDragon)) > 0)
                        {
                            AddBuff(attacker, target, new Buffs.SejuaniFrostResist(), 1, 1, frostDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        }
                    }
                }
                else
                {
                    if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniWintersClawChaos)) > 0)
                    {
                        AddBuff(attacker, target, new Buffs.SejuaniFrostTracker(), 1, 1, frostDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, target, new Buffs.SejuaniFrostChaos(nextBuffVars_MovementSpeedMod), 1, 1, frostDuration, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                        if(GetBuffCountFromCaster(target, target, nameof(Buffs.ResistantSkin)) > 0)
                        {
                            AddBuff(attacker, target, new Buffs.SejuaniFrostResistChaos(), 1, 1, frostDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        }
                        if(GetBuffCountFromCaster(target, target, nameof(Buffs.ResistantSkinDragon)) > 0)
                        {
                            AddBuff(attacker, target, new Buffs.SejuaniFrostResistChaos(), 1, 1, frostDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}