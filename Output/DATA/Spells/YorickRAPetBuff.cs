#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickRAPetBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MordekaiserChildrenOfTheGravePetBuff",
            BuffTextureName = "DarkChampion_EndlessRage.dds",
            IsPetDurationBuff = true,
        };
        float mordAP;
        float mordDmg;
        float[] effect0 = {0.75f, 0.75f, 0.75f};
        public override void OnActivate()
        {
            int level;
            float statMultiplier;
            float mordDmg;
            float mordAP;
            float mordHealth;
            float petHealth;
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            statMultiplier = this.effect0[level];
            mordDmg = GetTotalAttackDamage(attacker);
            mordAP = GetFlatMagicDamageMod(attacker);
            this.mordAP = statMultiplier * mordAP;
            this.mordDmg = statMultiplier * mordDmg;
            IncPermanentFlatPhysicalDamageMod(owner, this.mordDmg);
            IncPermanentFlatMagicDamageMod(owner, this.mordAP);
            mordHealth = GetMaxHealth(attacker, PrimaryAbilityResourceType.Shield);
            petHealth = 0.15f * mordHealth;
            if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.MordekaiserCOTGPetBuff2)) > 0)
            {
                IncPermanentFlatHPPoolMod(owner, petHealth);
            }
        }
    }
}