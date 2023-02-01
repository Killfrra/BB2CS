#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SummonerDot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SummonerIgnite",
            BuffTextureName = "SummonerIgnite.dds",
        };
        int level;
        Particle dotPart;
        float lastTimeExecuted;
        int[] effect0 = {14, 18, 22, 26, 30, 34, 38, 42, 46, 50, 54, 58, 62, 66, 70, 74, 78, 82};
        public SummonerDot(int level = default)
        {
            this.level = level;
        }
        public override void OnActivate()
        {
            //RequireVar(this.level);
            SpellEffectCreate(out this.dotPart, out _, "Summoner_Dot.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.dotPart);
        }
        public override void OnUpdateActions()
        {
            int level;
            level = this.level;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, attacker);
                if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.SummonerDot)) == 0)
                {
                    SpellEffectRemove(this.dotPart);
                }
            }
        }
    }
}
namespace Spells
{
    public class SummonerDot : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        int[] effect0 = {70, 90, 110, 130, 150, 170, 190, 210, 230, 250, 270, 290, 310, 330, 350, 370, 390, 410};
        public override void UpdateTooltip(int spellSlot)
        {
            float igniteDamage;
            float baseCooldown;
            float cooldownMultiplier;
            level = GetLevel(owner);
            igniteDamage = this.effect0[level];
            SetSpellToolTipVar(igniteDamage, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
            baseCooldown = 180;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            float cooldownMultiplier;
            float baseCooldown;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown = 180 * cooldownMultiplier;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Particle castParticle; // UNUSED
            int nextBuffVars_Level;
            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            level = GetLevel(owner);
            nextBuffVars_Level = level;
            AddBuff(attacker, target, new Buffs.SummonerDot(nextBuffVars_Level), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
    }
}