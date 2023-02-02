#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GravesPassiveCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "GravesPassiveCounter",
            BuffTextureName = "GravesTrueGrit.dds",
        };
        float lastTimeExecuted;
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.GravesPassiveGrit)) == 0)
            {
                AddBuff(attacker, owner, new Buffs.GravesPassiveGrit(), 11, 2, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
            charVars.ArmorAmountNeg = charVars.ArmorAmount * -1;
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, charVars.ArmorAmountNeg);
            IncFlatSpellBlockMod(owner, charVars.ArmorAmountNeg);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                int count;
                count = GetBuffCountFromAll(owner, nameof(Buffs.GravesPassiveGrit));
                if(count >= 11)
                {
                }
                else
                {
                    AddBuff(attacker, owner, new Buffs.GravesPassiveGrit(), 11, 1, 4, BuffAddType.STACKS_AND_CONTINUE, BuffType.COUNTER, 0, true, false, false);
                }
            }
        }
    }
}