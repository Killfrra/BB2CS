#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class InsanityPotion : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {35, 50, 65};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_Stats;
            nextBuffVars_Stats = this.effect0[level];
            AddBuff(attacker, target, new Buffs.InsanityPotion(nextBuffVars_Stats), 1, 1, 25, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellEffectCreate(out _, out _, "insanitypotion_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out _, out _, "insanitypotion_steam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "bottletip", default, target, default, default, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class InsanityPotion : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Insanity Potion",
            BuffTextureName = "ChemicalMan_ChemicalRage.dds",
        };
        float stats;
        float[] effect0 = {0.9f, 0.85f, 0.8f};
        public InsanityPotion(float stats = default)
        {
            this.stats = stats;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            int level;
            float cCreduction;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cCreduction = this.effect0[level];
            if(owner.Team != attacker.Team)
            {
                float percentReduction; // UNITIALIZED
                if(type == BuffType.SNARE)
                {
                    duration *= cCreduction;
                }
                if(type == BuffType.SLOW)
                {
                    duration *= cCreduction;
                }
                if(type == BuffType.FEAR)
                {
                    duration *= cCreduction;
                }
                if(type == BuffType.CHARM)
                {
                    duration *= cCreduction;
                }
                if(type == BuffType.SLEEP)
                {
                    duration *= cCreduction;
                }
                if(type == BuffType.STUN)
                {
                    duration *= cCreduction;
                }
                if(type == BuffType.TAUNT)
                {
                    duration *= cCreduction;
                }
                if(type == BuffType.SILENCE)
                {
                    duration *= percentReduction;
                }
                if(type == BuffType.BLIND)
                {
                    duration *= percentReduction;
                }
                duration = Math.Max(0.3f, duration);
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.stats);
        }
        public override void OnUpdateStats()
        {
            int level; // UNUSED
            float statsPercent; // UNUSED
            float statsPer5;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            statsPercent = this.stats / 100;
            statsPer5 = this.stats / 5;
            IncFlatSpellBlockMod(owner, this.stats);
            IncFlatMovementSpeedMod(owner, this.stats);
            IncFlatArmorMod(owner, this.stats);
            IncFlatMagicDamageMod(owner, this.stats);
            IncFlatHPRegenMod(owner, statsPer5);
            IncFlatPARRegenMod(owner, statsPer5, PrimaryAbilityResourceType.MANA);
        }
    }
}