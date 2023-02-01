#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BrandScorch : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon", "head", },
            AutoBuffActivateEffect = new[]{ "Flamesword.troy", "RighteousFuryHalo_buf.troy", },
            BuffName = "JudicatorRighteousFury",
            BuffTextureName = "Judicator_RighteousFury.dds",
        };
        public override void OnActivate()
        {
            SetDodgePiercing(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetDodgePiercing(owner, false);
        }
        public override void OnPreAttack()
        {
            RemoveOverrideAutoAttack(owner, false);
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    OverrideAutoAttack(3, SpellSlotType.ExtraSlots, owner, 1, true);
                }
            }
        }
    }
}