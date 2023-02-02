#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RaiseMorale : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {0.08f, 0.11f, 0.14f, 0.17f, 0.2f};
        int[] effect1 = {12, 19, 26, 33, 40};
        float[] effect2 = {0.04f, 0.055f, 0.07f, 0.085f, 0.1f};
        float[] effect3 = {6, 9.5f, 13, 16.5f, 20};
        public override void SelfExecute()
        {
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_AttackDmgMod;
            SpellBuffRemove(owner, nameof(Buffs.RaiseMorale), (ObjAIBase)owner, 0);
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            nextBuffVars_AttackDmgMod = this.effect1[level];
            AddBuff(attacker, attacker, new Buffs.RaiseMoraleTeamBuff(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackDmgMod), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            nextBuffVars_MoveSpeedMod = this.effect2[level];
            nextBuffVars_AttackDmgMod = this.effect3[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1500, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                AddBuff(attacker, unit, new Buffs.RaiseMoraleTeamBuff(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackDmgMod), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class RaiseMorale : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "RaiseMorale",
            BuffTextureName = "Pirate_RaiseMorale.dds",
            IsPetDurationBuff = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float[] effect0 = {0.03f, 0.04f, 0.05f, 0.06f, 0.07f};
        int[] effect1 = {8, 10, 12, 14, 16};
        public override void OnUpdateStats()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            IncPercentMovementSpeedMod(owner, this.effect0[level]);
            IncFlatPhysicalDamageMod(owner, this.effect1[level]);
        }
    }
}