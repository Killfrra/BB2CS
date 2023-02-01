#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SummonerTeleport : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Teleport",
            BuffTextureName = "Summoner_teleport.dds",
        };
        bool interrupted;
        Particle ak;
        Vector3 castPosition;
        float activateTime;
        int slotNum;
        float buffDuration;
        public SummonerTeleport(Vector3 castPosition = default, float buffDuration = default)
        {
            this.castPosition = castPosition;
            this.buffDuration = buffDuration;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(type == BuffType.SILENCE)
            {
                this.interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            else if(type == BuffType.FEAR)
            {
                this.interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            else if(type == BuffType.CHARM)
            {
                this.interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            else if(type == BuffType.SLEEP)
            {
                this.interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            else if(type == BuffType.STUN)
            {
                this.interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            else if(type == BuffType.TAUNT)
            {
                this.interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            else if(type == BuffType.SUPPRESSION)
            {
                this.interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            Particle castParticle; // UNUSED
            string name1;
            string name2;
            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.ak, out _, "Summoner_Teleport.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            //RequireVar(this.castPosition);
            name1 = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            this.activateTime = GetGameTime();
            if(name1 == nameof(Spells.SummonerTeleport))
            {
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, nameof(Spells.TeleportCancel));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 1);
                this.slotNum = 0;
            }
            else if(name2 == nameof(Spells.SummonerTeleport))
            {
                SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, nameof(Spells.TeleportCancel));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 1);
                this.slotNum = 1;
            }
            else
            {
                this.slotNum = 2;
            }
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            this.interrupted = false;
        }
        public override void OnDeactivate(bool expired)
        {
            float finishedTime;
            float totalTime;
            Vector3 castPosition;
            Particle akc; // UNUSED
            float cooldownMultiplier;
            float baseCooldown;
            //RequireVar(this.interrupted);
            //RequireVar(this.activateTime);
            finishedTime = GetGameTime();
            totalTime = finishedTime - this.activateTime;
            totalTime += 0.1f;
            if(!charVars.TeleportCancelled)
            {
                if(!this.interrupted)
                {
                    if(totalTime >= this.buffDuration)
                    {
                        castPosition = this.castPosition;
                        DestroyMissileForTarget(owner);
                        TeleportToPosition(owner, castPosition);
                        SpellEffectCreate(out akc, out _, "summoner_teleportarrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                        if(avatarVars.SummonerCooldownBonus != 0)
                        {
                            cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                            baseCooldown = 300 * cooldownMultiplier;
                        }
                        if(avatarVars.TeleportCooldownBonus != 0)
                        {
                            baseCooldown -= avatarVars.TeleportCooldownBonus;
                        }
                    }
                    else
                    {
                        baseCooldown = 180;
                    }
                }
                else
                {
                    baseCooldown = 180;
                }
            }
            else
            {
                baseCooldown = 180;
            }
            SpellEffectRemove(this.ak);
            SetCanMove(owner, true);
            SetCanCast(owner, true);
            SetGhosted(owner, false);
            SetCanAttack(owner, true);
            if(this.slotNum == 0)
            {
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, nameof(Spells.SummonerTeleport));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, baseCooldown);
            }
            else if(this.slotNum == 1)
            {
                SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, nameof(Spells.SummonerTeleport));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, baseCooldown);
            }
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}
namespace Spells
{
    public class SummonerTeleport : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void UpdateTooltip(int spellSlot)
        {
            float duration;
            float baseCooldown;
            float cooldownMultiplier;
            duration = 4;
            if(avatarVars.UtilityMastery == 1)
            {
                duration = 3.5f;
            }
            SetSpellToolTipVar(duration, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
            baseCooldown = 300;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            float cooldownMultiplier;
            float baseCooldown;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown = 300 * cooldownMultiplier;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 castPosition;
            Vector3 nextBuffVars_CastPosition;
            float nextBuffVars_BuffDuration;
            float duration;
            charVars.TeleportCancelled = false;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SummonerTeleport)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.SummonerTeleport), (ObjAIBase)owner, 0);
            }
            else
            {
                castPosition = GetRandomPointInAreaUnit(target, 100, 50);
                nextBuffVars_CastPosition = castPosition;
                if(avatarVars.UtilityMastery == 1)
                {
                    duration = 3.5f;
                }
                else
                {
                    duration = 4;
                }
                nextBuffVars_BuffDuration = duration;
                AddBuff((ObjAIBase)owner, owner, new Buffs.SummonerTeleport(nextBuffVars_CastPosition, nextBuffVars_BuffDuration), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                if(target is ObjAIBase)
                {
                    if(target is BaseTurret)
                    {
                        AddBuff(attacker, target, new Buffs.Teleport_Turret(), 1, 1, nextBuffVars_BuffDuration, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, target, new Buffs.Teleport_Target(), 1, 1, 0.1f + nextBuffVars_BuffDuration, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                }
                AddBuff((ObjAIBase)target, owner, new Buffs.Teleport_DeathRemoval(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if(GetBuffCountFromCaster(target, default, nameof(Buffs.SharedWardBuff)) > 0)
                {
                    AddBuff(attacker, target, new Buffs.Destealth(), 1, 1, 1 + nextBuffVars_BuffDuration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}