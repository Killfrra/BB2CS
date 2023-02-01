#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WildCards : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "head", },
            AutoBuffActivateEffect = new[]{ "SealFate_tar.prt", "GLOBAL_SILENCE.TROY", },
            BuffName = "Wild Cards",
            BuffTextureName = "Cardmaster_PowerCard.dds",
            NonDispellable = true,
        };
        int[] effect0 = {60, 110, 160, 210, 260};
        public override void OnSpellHit()
        {
            int level;
            attacker = SetBuffCasterUnit();
            if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.SealFateMissile)) > 0)
            {
            }
            else
            {
                AddBuff(attacker, target, new Buffs.SealFateMissile(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.65f, 1, false, false, attacker);
            }
        }
    }
}
namespace Spells
{
    public class WildCards : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 pos;
            Vector3 pos1;
            Minion other1;
            Vector3 pos2;
            Vector3 pos3;
            StopChanneling((ObjAIBase)owner, ChannelingStopCondition.NotCancelled, ChannelingStopSource.NotCancelled);
            teamID = GetTeamID(owner);
            pos = GetPointByUnitFacingOffset(owner, 30, 0);
            pos1 = GetPointByUnitFacingOffset(owner, 1000, 0);
            other1 = SpawnMinion("TestCube", "TestCubeRender", "idle.lua", pos, teamID, false, true, false, true, false, true, 0, false, true, (Champion)owner);
            pos2 = GetPointByUnitFacingOffset(owner, 1000, -28);
            pos3 = GetPointByUnitFacingOffset(owner, 1000, 28);
            SetSpell(other1, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.SealFateMissile));
            SpellCast(other1, default, pos1, pos1, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            SpellCast(other1, default, pos2, pos2, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            SpellCast(other1, default, pos3, pos3, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            AddBuff(attacker, other1, new Buffs.WildCards(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 6, true, false, false);
            AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 6, true, false, false);
        }
    }
}