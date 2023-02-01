#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AlZaharVoidlingPhase2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AlZaharVoidlingPhase2",
            BuffTextureName = "AlZahar_SummonVoidling.dds",
        };
        float damageInc;
        float armorInc;
        public override void OnActivate()
        {
            Particle varrr; // UNUSED
            float baseArmor;
            float baseDamage;
            SpellEffectCreate(out varrr, out _, "voidlingtransform.prt", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            baseArmor = GetArmor(owner);
            baseDamage = GetTotalAttackDamage(owner);
            this.damageInc = baseDamage * 0.5f;
            this.armorInc = baseArmor * 0.5f;
            IncScaleSkinCoef(0.5f, owner);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.damageInc);
            IncFlatArmorMod(owner, this.armorInc);
            IncScaleSkinCoef(0.5f, owner);
        }
    }
}