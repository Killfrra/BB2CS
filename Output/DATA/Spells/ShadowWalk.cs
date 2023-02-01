#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShadowWalk : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "ShadowWalk",
            BuffTextureName = "Evelynn_ReadyToBetray.dds",
            SpellToggleSlot = 2,
        };
        float moveSpeedMod;
        bool willRemove;
        TeamId teamID;
        Fade iD; // UNUSED
        int[] effect0 = {12, 11, 10, 9, 8};
        public ShadowWalk(float moveSpeedMod = default, bool willRemove = default, TeamId teamID = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.willRemove = willRemove;
            this.teamID = teamID;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.willRemove);
            this.teamID = GetTeamID(owner);
            this.iD = PushCharacterFade(owner, 0.2f, 0.1f);
            SetStealthed(owner, true);
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, -60, PrimaryAbilityResourceType.MANA);
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            float baseCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            owner = SetBuffCasterUnit();
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetStealthed(owner, false);
            baseCooldown = this.effect0[level];
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * baseCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            this.iD = PushCharacterFade(owner, 1, 0.5f);
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            float nextBuffVars_MoveSpeedMod;
            TeamId nextBuffVars_TeamID;
            int nextBuffVars_BreakDamage;
            spellName = GetSpellName();
            if(spellName == nameof(Spells.Ravage))
            {
                nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                nextBuffVars_TeamID = this.teamID;
                nextBuffVars_BreakDamage = 0;
                AddBuff((ObjAIBase)owner, owner, new Buffs.WasStealthed(nextBuffVars_MoveSpeedMod, nextBuffVars_BreakDamage), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if(spellName == nameof(Spells.HateSpike))
            {
                nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                nextBuffVars_TeamID = this.teamID;
                nextBuffVars_BreakDamage = 0;
                AddBuff((ObjAIBase)owner, owner, new Buffs.WasStealthed(nextBuffVars_MoveSpeedMod, nextBuffVars_BreakDamage), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if(spellName != nameof(Spells.ShadowWalk))
            {
                if(spellVars.CastingBreaksStealth)
                {
                    this.willRemove = true;
                }
                else if(!spellVars.CastingBreaksStealth)
                {
                }
                else if(!spellVars.DoesntTriggerSpellCasts)
                {
                    this.willRemove = true;
                }
            }
        }
        public override void OnPreAttack()
        {
            float nextBuffVars_MoveSpeedMod;
            TeamId nextBuffVars_TeamID;
            float nextBuffVars_BreakDamage;
            nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
            nextBuffVars_TeamID = this.teamID;
            nextBuffVars_BreakDamage = 0;
            AddBuff(attacker, owner, new Buffs.WasStealthed(nextBuffVars_MoveSpeedMod, nextBuffVars_BreakDamage), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Spells
{
    public class ShadowWalk : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        TeamId teamID; // UNITIALIZED
        float[] effect0 = {-0.3f, -0.35f, -0.4f, -0.45f, -0.5f};
        int[] effect1 = {10, 20, 30, 40, 50};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            bool temp;
            Particle particle; // UNUSED
            float nextBuffVars_InitialTime;
            float nextBuffVars_TimeLastHit;
            float nextBuffVars_MoveSpeedMod;
            int nextBuffVars_StealthDuration;
            TeamId nextBuffVars_TeamID;
            bool nextBuffVars_WillRemove;
            teamID = GetTeamID(owner);
            temp = GetStealthed(owner);
            if(temp)
            {
                SpellBuffRemove(owner, nameof(Buffs.ShadowWalk), (ObjAIBase)owner, 0);
                SpellBuffRemove(owner, nameof(Buffs.ShadowWalk_internal), (ObjAIBase)owner, 0);
            }
            else
            {
                SpellEffectCreate(out particle, out _, "evelyn_invis_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                nextBuffVars_InitialTime = GetTime();
                nextBuffVars_TimeLastHit = GetTime();
                nextBuffVars_MoveSpeedMod = this.effect0[level];
                nextBuffVars_StealthDuration = this.effect1[level];
                nextBuffVars_TeamID = this.teamID;
                nextBuffVars_WillRemove = false;
                AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalk_internal(nextBuffVars_TimeLastHit, nextBuffVars_MoveSpeedMod, nextBuffVars_StealthDuration, nextBuffVars_TeamID), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}