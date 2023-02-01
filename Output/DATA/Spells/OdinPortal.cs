#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinPortal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShrineAura",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            float _0_5; // UNITIALIZED
            int count;
            int count2;
            int count3;
            if(ExecutePeriodically(0, ref this.lastTimeExecuted, false, _0_5))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 250, SpellDataFlags.AffectHeroes, default, true))
                {
                    count = GetBuffCountFromAll(unit, nameof(Buffs.OdinPortalMoveCheck));
                    if(count == 0)
                    {
                        count2 = GetBuffCountFromAll(unit, nameof(Buffs.OdinPortalChannel));
                        if(count2 == 0)
                        {
                            count3 = GetBuffCountFromAll(unit, nameof(Buffs.OdinPortalTeleport));
                            if(count3 == 0)
                            {
                                AddBuff((ObjAIBase)unit, unit, new Buffs.OdinPortalMoveCheck(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}