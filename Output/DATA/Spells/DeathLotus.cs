#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DeathLotus : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "",
            BuffTextureName = "",
        };
        int level;
        public DeathLotus(int level = default)
        {
            this.level = level;
        }
        public override void OnUpdateActions()
        {
            int level;
            level = this.level;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 3, default, true))
            {
                SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
        }
    }
}
namespace Spells
{
    public class DeathLotus : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 2.65f,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            returnValue = false;
            foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            int nextBuffVars_Level;
            AddBuff((ObjAIBase)owner, owner, new Buffs.DeathLotusSound(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            nextBuffVars_Level = level;
            AddBuff((ObjAIBase)owner, owner, new Buffs.DeathLotus(nextBuffVars_Level), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0.25f, true, false);
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.DeathLotusSound), (ObjAIBase)owner);
            SpellBuffRemove(owner, nameof(Buffs.DeathLotus), (ObjAIBase)owner);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.DeathLotusSound), (ObjAIBase)owner);
            SpellBuffRemove(owner, nameof(Buffs.DeathLotus), (ObjAIBase)owner);
        }
    }
}