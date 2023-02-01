#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GravesPassiveGrit : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "GravesTrueGrit.troy", },
            BuffName = "GravesPassiveGrit",
            BuffTextureName = "GravesTrueGrit.dds",
        };
        float lastTimeExecuted;
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.GravesPassiveGrit));
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, charVars.ArmorAmount);
            IncFlatSpellBlockMod(owner, charVars.ArmorAmount);
        }
        public override void OnUpdateActions()
        {
            float count;
            float total;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                count = GetBuffCountFromAll(owner, nameof(Buffs.GravesPassiveGrit));
                count--;
                total = count * charVars.ArmorAmount;
                SetBuffToolTipVar(1, total);
            }
        }
        public override void OnUpdateAmmo()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.GravesPassiveCounter)) == 0)
            {
                SpellBuffClear(owner, nameof(Buffs.GravesPassiveGrit));
            }
        }
    }
}