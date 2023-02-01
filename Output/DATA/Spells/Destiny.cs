#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Destiny : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "DestinyEye.troy", "", },
            BuffName = "Destiny",
            BuffTextureName = "Destiny_temp.dds",
        };
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId casterID;
            casterID = GetTeamID(attacker);
            this.bubbleID = AddUnitPerceptionBubble(casterID, 1000, owner, 40, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
        }
    }
}
namespace Spells
{
    public class Destiny : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 150f, 135f, 120f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.75f,
            SpellDamageRatio = 0.75f,
        };
        int[] effect0 = {6, 8, 10};
        int[] effect1 = {6, 8, 10};
        public override void SelfExecute()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.Destiny_marker(), 1, 1, this.effect0[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.Destiny(), 1, 1, this.effect1[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.Gate));
            SetSlotSpellCooldownTimeVer2(0.5f, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}