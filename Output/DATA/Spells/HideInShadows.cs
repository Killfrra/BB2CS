#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HideInShadows : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Hide",
            BuffTextureName = "Twitch_AlterEgo.dds",
            SpellToggleSlot = 1,
        };
        float attackSpeedMod;
        bool willRemove;
        Fade iD; // UNUSED
        float initialTime;
        public HideInShadows(float attackSpeedMod = default, bool willRemove = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.willRemove);
            this.iD = PushCharacterFade(owner, 0.2f, 0.1f);
            SetStealthed(owner, true);
            this.initialTime = GetTime();
            SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, -60, PrimaryAbilityResourceType.MANA);
        }
        public override void OnDeactivate(bool expired)
        {
            float baseCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            float curTime;
            float timeSinceLast;
            float nextBuffVars_AttackSpeedMod;
            SetStealthed(owner, false);
            baseCooldown = 11;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * baseCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            this.iD = PushCharacterFade(owner, 1, 0.5f);
            SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            curTime = GetTime();
            timeSinceLast = curTime - this.initialTime;
            timeSinceLast *= 2;
            timeSinceLast = Math.Min(timeSinceLast, 10);
            nextBuffVars_AttackSpeedMod = this.attackSpeedMod;
            AddBuff((ObjAIBase)owner, owner, new Buffs.HideInShadowsBuff(nextBuffVars_AttackSpeedMod), 1, 1, 0.5f + timeSinceLast, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HideInShadowsBuff)) == 0)
            {
                IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
            }
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            spellName = GetSpellName();
            if(spellName != nameof(Spells.HideInShadows))
            {
                if(spellVars.CastingBreaksStealth)
                {
                    this.willRemove = true;
                    SpellBuffRemoveCurrent(owner);
                }
                else if(!spellVars.CastingBreaksStealth)
                {
                }
                else if(!spellVars.DoesntTriggerSpellCasts)
                {
                    this.willRemove = true;
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnDeath()
        {
            if(owner.IsDead)
            {
                this.willRemove = true;
            }
        }
        public override void OnLaunchAttack()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}
namespace Spells
{
    public class HideInShadows : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.3f, 0.4f, 0.5f, 0.6f, 0.7f};
        int[] effect1 = {10, 20, 30, 40, 50};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            Particle nextBuffVars_a;
            float nextBuffVars_InitialTime;
            float nextBuffVars_TimeLastHit;
            float nextBuffVars_AttackSpeedMod;
            int nextBuffVars_StealthDuration;
            teamID = GetTeamID(owner);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HideInShadows)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.HideInShadows), (ObjAIBase)owner);
                SpellBuffRemove(owner, nameof(Buffs.HideInShadows_internal), (ObjAIBase)owner);
            }
            else
            {
                SpellEffectCreate(out nextBuffVars_a, out _, "twitch_invis_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                nextBuffVars_InitialTime = GetTime();
                nextBuffVars_TimeLastHit = GetTime();
                nextBuffVars_AttackSpeedMod = this.effect0[level];
                nextBuffVars_StealthDuration = this.effect1[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.HideInShadows_internal(nextBuffVars_TimeLastHit, nextBuffVars_AttackSpeedMod, nextBuffVars_StealthDuration), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}