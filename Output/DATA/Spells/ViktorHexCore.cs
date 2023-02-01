#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorHexCore : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            int ownerLevel;
            charVars.ManaToADD = 0;
            charVars.HealthToADD = 0;
            ownerLevel = GetLevel(owner);
            charVars.BonusForItem = ownerLevel * 3;
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            float manaCost;
            float maxHealth;
            float currHealth; // UNUSED
            float maxHealthReturn; // UNUSED
            float maxPAR;
            float currPAR; // UNUSED
            float maxPARReturn; // UNUSED
            float currentADDMana;
            float currentADDHealth;
            manaCost = GetPARCost();
            maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            currHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            maxHealthReturn = maxHealth * 0.2f;
            maxPAR = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            currPAR = GetPAR(owner, PrimaryAbilityResourceType.MANA);
            maxPARReturn = maxPAR * 0.2f;
            currentADDMana = manaCost * 0.1f;
            currentADDHealth = manaCost * 0.1f;
            charVars.ManaToADD += currentADDMana;
            charVars.HealthToADD += currentADDHealth;
        }
        public override void OnLevelUp()
        {
            int ownerLevel;
            ownerLevel = GetLevel(owner);
            charVars.BonusForItem = ownerLevel * 3;
        }
    }
}