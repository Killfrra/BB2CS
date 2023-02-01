#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SummonerBattleCry : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", "", },
            AutoBuffActivateEffect = new[]{ "Summoner_cast.troy", "summoner_battlecry.troy", "summoner_battlecry_oc.troy", "", },
            BuffName = "SummonerBattleCry",
            BuffTextureName = "Summoner_BattleCry.dds",
        };
        float scaleCoef;
        float scaleCap;
        float aPMod;
        float attackSpeedMod;
        public SummonerBattleCry(float scaleCoef = default, float scaleCap = default, float aPMod = default, float attackSpeedMod = default)
        {
            this.scaleCoef = scaleCoef;
            this.scaleCap = scaleCap;
            this.aPMod = aPMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            float duration; // UNUSED
            //RequireVar(this.scaleCoef);
            //RequireVar(this.scaleCap);
            IncScaleSkinCoef(this.scaleCoef, owner);
            //RequireVar(this.aPMod);
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.allyAttackSpeedMod);
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
            IncFlatMagicDamageMod(owner, this.aPMod);
            duration = GetBuffRemainingDuration(owner, nameof(Buffs.SummonerBattleCry));
        }
        public override void OnDeactivate(bool expired)
        {
            Particle ee; // UNUSED
            SpellEffectCreate(out ee, out _, "summoner_battlecry_obd.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, false, false, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
            IncFlatMagicDamageMod(owner, this.aPMod);
            if(this.scaleCap < 4)
            {
                this.scaleCoef += 0.04f;
                this.scaleCap++;
            }
            IncScaleSkinCoef(this.scaleCoef, owner);
        }
    }
}
namespace Spells
{
    public class SummonerBattleCry : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 90f, 90f, 90f, },
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {10, 14, 18, 22, 26, 30, 34, 38, 42, 48, 52, 54, 58, 62, 66, 70, 74, 78};
        float[] effect1 = {0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f};
        int[] effect2 = {10, 14, 18, 22, 26, 30, 34, 38, 42, 48, 52, 54, 58, 62, 66, 70, 74, 78};
        float[] effect3 = {0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f};
        int[] effect4 = {12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12};
        public override void UpdateTooltip(int spellSlot)
        {
            float aPMod;
            float attackSpeedMod;
            float baseCooldown;
            float cooldownMultiplier;
            level = GetLevel(owner);
            aPMod = this.effect0[level];
            attackSpeedMod = this.effect1[level];
            if(avatarVars.OffensiveMastery == 1)
            {
                aPMod *= 1.1f;
                attackSpeedMod += 0.05f;
            }
            attackSpeedMod *= 100;
            SetSpellToolTipVar(attackSpeedMod, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
            SetSpellToolTipVar(aPMod, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
            baseCooldown = 180;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 3, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            float baseCooldown;
            float cooldownMultiplier;
            baseCooldown = 180;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_ScaleCoef;
            float nextBuffVars_ScaleCap;
            float nextBuffVars_APMod;
            float nextBuffVars_AttackSpeedMod;
            Particle castParticle; // UNUSED
            nextBuffVars_ScaleCoef = 0.04f;
            nextBuffVars_ScaleCap = 0;
            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            level = GetLevel(owner);
            nextBuffVars_APMod = this.effect2[level];
            nextBuffVars_AttackSpeedMod = this.effect3[level];
            if(avatarVars.OffensiveMastery == 1)
            {
                nextBuffVars_APMod *= 1.1f;
                nextBuffVars_AttackSpeedMod += 0.05f;
            }
            AddBuff(attacker, attacker, new Buffs.SummonerBattleCry(nextBuffVars_ScaleCoef, nextBuffVars_ScaleCap, nextBuffVars_APMod, nextBuffVars_AttackSpeedMod), 1, 1, this.effect4[level], BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true, false, false);
        }
    }
}