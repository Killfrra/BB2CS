﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AegisoftheLegionAuraSelf : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "", "", },
            AutoBuffActivateEffect = new[]{ "Aegis_buf.troy", "", "", },
            BuffName = "Aegis of the Legion",
            BuffTextureName = "034_Steel_Shield.dds",
        };
        float magicResistBonus;
        float armorBonus;
        float damageBonus;
        public AegisoftheLegionAuraSelf(float magicResistBonus = default, float armorBonus = default, float damageBonus = default)
        {
            this.magicResistBonus = magicResistBonus;
            this.armorBonus = armorBonus;
            this.damageBonus = damageBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.magicResistBonus);
            //RequireVar(this.armorBonus);
            //RequireVar(this.damageBonus);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, this.magicResistBonus);
            IncFlatArmorMod(owner, this.armorBonus);
            IncFlatPhysicalDamageMod(owner, this.damageBonus);
        }
    }
}