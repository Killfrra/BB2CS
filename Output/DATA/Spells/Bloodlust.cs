#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Bloodlust : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 30f, 26f, 22f, 18f, 14f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {30, 40, 50, 60, 70};
        float[] effect1 = {0.5f, 0.95f, 1.4f, 1.85f, 2.3f};
        public override void SelfExecute()
        {
            float currentFury;
            float baseHeal;
            float healthPerFury;
            float healthToRestore;
            float spellPower;
            float abilityPowerMod;
            Particle part; // UNUSED
            float furyToRemove;
            currentFury = GetPAR(owner, PrimaryAbilityResourceType.Other);
            baseHeal = this.effect0[level];
            healthPerFury = this.effect1[level];
            healthToRestore = currentFury * healthPerFury;
            healthToRestore += baseHeal;
            spellPower = GetFlatMagicDamageMod(owner);
            abilityPowerMod = 1.5f * spellPower;
            healthToRestore += abilityPowerMod;
            IncHealth(owner, healthToRestore, owner);
            SpellEffectCreate(out part, out _, "Tryndamere_Heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            furyToRemove = -1 * currentFury;
            IncPAR(owner, furyToRemove, PrimaryAbilityResourceType.Other);
        }
    }
}
namespace Buffs
{
    public class Bloodlust : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "Bloodlust",
            BuffTextureName = "DarkChampion_Bloodlust.dds",
            SpellFXOverrideSkins = new[]{ "TryndamereDemonsword", },
            SpellToggleSlot = 1,
        };
        float damageMod;
        float critDamageMod;
        public Bloodlust(float damageMod = default, float critDamageMod = default)
        {
            this.damageMod = damageMod;
            this.critDamageMod = critDamageMod;
        }
        public override void OnActivate()
        {
            int count;
            float totalDamage;
            float totalCritDamage;
            //RequireVar(this.damageMod);
            //RequireVar(this.critDamageMod);
            count = GetBuffCountFromAll(owner, nameof(Buffs.Bloodlust));
            totalDamage = count * this.damageMod;
            totalCritDamage = count * this.critDamageMod;
            totalCritDamage *= 100;
            SetBuffToolTipVar(1, totalDamage);
            SetBuffToolTipVar(2, totalCritDamage);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.damageMod);
            IncFlatCritDamageMod(owner, this.critDamageMod);
        }
    }
}