#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneUmbraBlades : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "BUFFBONE_CSTM_L_PALM", "BUFFBONE_CSTM_R_PALM", },
            AutoBuffActivateEffect = new[]{ "NocturnePassiveReady.troy", "NocturnePassiveReady.troy", },
            BuffName = "NocturneUmbraBlades",
            BuffTextureName = "Nocturne_UmbraBlades.dds",
        };
        int[] effect0 = {15, 15, 15, 15, 15, 15, 20, 20, 20, 20, 20, 20, 25, 25, 25, 25, 25, 25};
        public override void OnActivate()
        {
            int level;
            float heal;
            SetDodgePiercing(owner, true);
            level = GetLevel(owner);
            heal = this.effect0[level];
            SetBuffToolTipVar(1, heal);
        }
        public override void OnDeactivate(bool expired)
        {
            charVars.Count = 0;
            SetDodgePiercing(owner, false);
            RemoveOverrideAutoAttack(owner, true);
        }
        public override void OnPreAttack()
        {
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                    RemoveOverrideAutoAttack(owner, true);
                }
                else
                {
                    OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
                }
            }
            else
            {
                RemoveOverrideAutoAttack(owner, true);
            }
        }
    }
}