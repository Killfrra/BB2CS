#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GatlingGun : BBBuffScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            Vector3 pos;
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                pos = GetPointByUnitFacingOffset(owner, 300, -15);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
                pos = GetPointByUnitFacingOffset(owner, 300, 0);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
                pos = GetPointByUnitFacingOffset(owner, 300, 5);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
                pos = GetPointByUnitFacingOffset(owner, 300, -5);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
                pos = GetPointByUnitFacingOffset(owner, 300, 10);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
                pos = GetPointByUnitFacingOffset(owner, 300, -10);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
                pos = GetPointByUnitFacingOffset(owner, 300, 15);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
            }
        }
    }
}
namespace Spells
{
    public class GatlingGun : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 20f, 16f, 12f, 8f, 4f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {4, 4.5f, 5, 5.5f, 6};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            AddBuff(attacker, target, new Buffs.GatlingGun(), 1, 1, this.effect0[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}