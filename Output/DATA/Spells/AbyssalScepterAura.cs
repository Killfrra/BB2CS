#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AbyssalScepterAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Abyssal Scepter",
            BuffTextureName = "3001_Abyssal_Scepter.dds",
        };
        float magicResistanceMod;
        public AbyssalScepterAura(float magicResistanceMod = default)
        {
            this.magicResistanceMod = magicResistanceMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.magicResistanceMod);
        }
        public override void OnUpdateStats()
        {
            float dist;
            IncFlatSpellBlockMod(owner, this.magicResistanceMod);
            if(attacker.IsDead)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                dist = DistanceBetweenObjects("Attacker", "Owner");
                if(dist >= 1000)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}