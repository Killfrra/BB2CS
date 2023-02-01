#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class PoppyParagonOfDemacia : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 4, 4, 4, 4, 4, },
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonParticle(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonSpeed(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonIcon(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}