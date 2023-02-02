#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Highlander : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 75f, 75f, 75f, 18f, 14f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.4f, 0.6f, 0.8f};
        int[] effect1 = {6, 9, 12};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            nextBuffVars_MoveSpeedMod = 0.4f;
            nextBuffVars_AttackSpeedMod = this.effect0[level];
            AddBuff(attacker, target, new Buffs.Highlander(nextBuffVars_AttackSpeedMod, nextBuffVars_MoveSpeedMod), 1, 1, this.effect1[level], BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class Highlander : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Highlander_buf.troy", },
            BuffTextureName = "MasterYi_InnerFocus2.dds",
        };
        float attackSpeedMod;
        float moveSpeedMod;
        int[] effect0 = {9, 8, 7, 6, 5};
        public Highlander(float attackSpeedMod = default, float moveSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.SNARE)
                {
                    Say(owner, "game_lua_Highlander");
                    returnValue = false;
                }
                if(type == BuffType.SLOW)
                {
                    Say(owner, "game_lua_Highlander");
                    returnValue = false;
                }
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
        }
        public override void OnKill()
        {
            if(target is Champion)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                SpellEffectCreate(out _, out _, "DeathsCaress_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            }
        }
        public override void OnAssist()
        {
            object level; // UNITIALIZED
            if(target is Champion)
            {
                float alphaStrikeCD;
                float wujuStyleCD;
                float highlanderCD;
                float meditateCD;
                float aSCDLeft;
                float medCDLeft;
                float wujuCDLeft;
                float highCDLeft;
                float aSCDFinal;
                float medCDFinal;
                float wujuCDFinal;
                float highCDFinal;
                alphaStrikeCD = this.effect0[level];
                wujuStyleCD = 12.5f;
                highlanderCD = 37.5f;
                meditateCD = 22.5f;
                aSCDLeft = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                medCDLeft = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                wujuCDLeft = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                highCDLeft = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                aSCDFinal = aSCDLeft - alphaStrikeCD;
                medCDFinal = medCDLeft - meditateCD;
                wujuCDFinal = wujuCDLeft - wujuStyleCD;
                highCDFinal = highCDLeft - highlanderCD;
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, aSCDFinal);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, medCDFinal);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, wujuCDFinal);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, highCDFinal);
                SpellEffectCreate(out _, out _, "DeathsCaress_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            }
        }
    }
}