#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GrievousWound : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "Head", },
            AutoBuffActivateEffect = new[]{ "Global_Mortal_Strike.troy", },
            BuffName = "GrievousWound",
            BuffTextureName = "GW_Debuff.dds",
        };
        float lifeStealMod;
        float spellVampMod;
        public override void OnActivate()
        {
            float lifeStealMod;
            float spellVampMod;
            ApplyAssistMarker(attacker, owner, 10);
            lifeStealMod = GetPercentLifeStealMod(owner);
            this.lifeStealMod = lifeStealMod * -0.5f;
            spellVampMod = GetPercentSpellBlockMod(owner);
            this.spellVampMod = spellVampMod * -0.5f;
        }
        public override void OnUpdateStats()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Internal_50MS)) > 0)
            {
                IncPercentHPRegenMod(owner, -0.5f);
                IncPercentLifeStealMod(owner, this.lifeStealMod);
                IncPercentSpellVampMod(owner, this.spellVampMod);
            }
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            float effectiveHeal;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Internal_50MS)) > 0)
            {
                if(health >= 0)
                {
                    effectiveHeal = health * 0.5f;
                    returnValue = effectiveHeal;
                }
            }
            return returnValue;
        }
        public override void OnUpdateActions()
        {
            float lifeStealMod;
            float spellVampMod;
            lifeStealMod = GetPercentLifeStealMod(owner);
            lifeStealMod -= this.lifeStealMod;
            this.lifeStealMod = lifeStealMod * -0.5f;
            spellVampMod = GetPercentSpellBlockMod(owner);
            spellVampMod -= this.spellVampMod;
            this.spellVampMod = spellVampMod * -0.5f;
        }
    }
}