#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CorkiMissileBarrageCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateActions()
        {
            int count;
            if(!owner.IsDead)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.CorkiMissileBarrageTimer)) == 0)
                {
                    count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.MissileBarrage));
                    if(count != 7)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.CorkiMissileBarrageTimer(), 1, 1, charVars.ChargeCooldown, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    }
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            int spellSlot;
            spellSlot = GetSpellSlot();
            if(spellSlot == 3)
            {
                charVars.BarrageCounter++;
            }
        }
    }
}