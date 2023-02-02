#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaGhost : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "OrianaGhost",
            BuffTextureName = "OriannaPassive.dds",
            PersistsThroughDeath = true,
        };
        ObjAIBase caster;
        bool ghostSpawned;
        bool minionBall;
        Particle temp;
        Particle orianaPointer;
        int previousState;
        int currentState;
        int[] effect0 = {10, 15, 20, 25, 30};
        public override void OnActivate()
        {
            Vector3 currentPos;
            ObjAIBase caster;
            string skinName;
            Vector3 attackerPos; // UNUSED
            float distance;
            currentPos = GetUnitPosition(owner);
            caster = SetBuffCasterUnit();
            this.caster = SetBuffCasterUnit();
            this.ghostSpawned = false;
            this.minionBall = false;
            skinName = GetUnitSkinName(owner);
            if(skinName == "OriannaBall")
            {
                this.minionBall = true;
            }
            if(skinName == "OriannaBall")
            {
                this.minionBall = true;
            }
            if(!this.minionBall)
            {
                if(caster != owner)
                {
                    int skinID;
                    skinID = GetSkinID(caster);
                    if(skinID == 1)
                    {
                        SpellEffectCreate(out this.temp, out _, "Oriana_ghost_bind_goth.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, currentPos, owner, default, currentPos, false, false, false, false, false);
                    }
                    else if(skinID == 2)
                    {
                        SpellEffectCreate(out this.temp, out _, "Oriana_ghost_bind_doll.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, currentPos, owner, default, currentPos, false, false, false, false, false);
                    }
                    else
                    {
                        SpellEffectCreate(out this.temp, out _, "Oriana_Ghost_bind.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, currentPos, owner, default, currentPos, false, false, false, false, false);
                    }
                }
            }
            attackerPos = GetUnitPosition(attacker);
            caster = SetBuffCasterUnit();
            distance = DistanceBetweenObjects("Caster", "Owner");
            if(distance >= 1000)
            {
                SpellEffectCreate(out this.orianaPointer, out _, "OrianaBallIndicatorFar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, attacker, default, default, owner, default, default, false, false, false, false, true, owner);
                this.previousState = 0;
            }
            else if(distance >= 800)
            {
                SpellEffectCreate(out this.orianaPointer, out _, "OrianaBallIndicatorMedium.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, attacker, default, default, owner, default, default, false, false, false, false, true, owner);
                this.previousState = 1;
            }
            else if(distance >= 0)
            {
                SpellEffectCreate(out this.orianaPointer, out _, "OrianaBallIndicatorNear.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, attacker, default, default, owner, default, default, false, false, false, false, true, owner);
                this.previousState = 2;
            }
            SetSpellOffsetTarget(1, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, attacker, owner);
            SetSpellOffsetTarget(3, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, attacker, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            AttackableUnit caster;
            string skinName;
            SpellEffectRemove(this.orianaPointer);
            SpellBuffClear(owner, nameof(Buffs.OrianaGhostMinion));
            caster = this.caster;
            skinName = GetUnitSkinName(owner);
            if(skinName != "OriannaBall")
            {
                if(caster != owner)
                {
                    SpellEffectRemove(this.temp);
                }
            }
            else
            {
                SpellBuffClear(attacker, nameof(Buffs.OriannaBallTracker));
            }
        }
        public override void OnUpdateStats()
        {
            ObjAIBase caster;
            int level;
            float distance;
            caster = SetBuffCasterUnit();
            level = GetSlotSpellLevel(caster, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                float defenseBonus;
                defenseBonus = this.effect0[level];
                IncFlatArmorMod(owner, defenseBonus);
                IncFlatSpellBlockMod(owner, defenseBonus);
            }
            caster = SetBuffCasterUnit();
            distance = DistanceBetweenObjects("Caster", "Owner");
            if(distance >= 1000)
            {
                this.currentState = 0;
            }
            else if(distance >= 800)
            {
                this.currentState = 1;
            }
            else if(distance >= 0)
            {
                this.currentState = 2;
            }
            if(this.currentState != this.previousState)
            {
                SpellEffectRemove(this.orianaPointer);
                if(this.currentState == 0)
                {
                    SpellEffectCreate(out this.orianaPointer, out _, "OrianaBallIndicatorFar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, attacker, default, default, owner, default, default, false, false, false, false, true, owner);
                }
                else if(this.currentState == 1)
                {
                    SpellEffectCreate(out this.orianaPointer, out _, "OrianaBallIndicatorMedium.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, attacker, default, default, owner, default, default, false, false, false, false, true, owner);
                }
                else
                {
                    SpellEffectCreate(out this.orianaPointer, out _, "OrianaBallIndicatorNear.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, attacker, default, default, owner, default, default, false, false, false, false, true, owner);
                }
            }
            this.previousState = this.currentState;
        }
        public override void OnUpdateActions()
        {
            ObjAIBase caster;
            float distance;
            Vector3 castPos;
            TeamId teamID;
            Particle temp; // UNUSED
            caster = SetBuffCasterUnit();
            distance = DistanceBetweenObjects("Caster", "Owner");
            if(distance > 1125)
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(1, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(2, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(3, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
                castPos = GetUnitPosition(owner);
                teamID = GetTeamID(owner);
                SpellEffectCreate(out temp, out _, "Orianna_Ball_Flash.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", castPos, owner, default, default, true, false, false, false, false);
                AddBuff(caster, caster, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                SpellEffectCreate(out temp, out _, "Orianna_Ball_Flash_Reverse.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, caster, false, caster, "SpinnigBottomRidge", castPos, caster, default, default, true, false, false, false, false);
                SealSpellSlot(0, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(1, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(2, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(3, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            else if(distance <= 25)
            {
                if(GetBuffCountFromCaster(caster, default, nameof(Buffs.OrianaDissonanceCountdown)) == 0)
                {
                    if(owner is not Champion)
                    {
                        caster = SetBuffCasterUnit();
                        AddBuff(caster, caster, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
                        SpellEffectCreate(out temp, out _, "Orianna_Ball_Flash_Reverse.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, caster, false, caster, "SpinnigBottomRidge", castPos, caster, default, default, true, false, false, false, false);
                    }
                }
            }
            else
            {
                bool noRender;
                noRender = GetNoRender(owner);
                if(owner is Champion)
                {
                    if(noRender)
                    {
                        SealSpellSlot(0, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                        SealSpellSlot(1, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                        SealSpellSlot(2, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                        SealSpellSlot(3, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                        SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
                        castPos = GetUnitPosition(owner);
                        teamID = GetTeamID(owner);
                        SpellEffectCreate(out temp, out _, "Orianna_Ball_Flash.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", castPos, owner, default, default, true, false, false, false, false);
                        AddBuff(caster, caster, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                        SpellEffectCreate(out temp, out _, "Orianna_Ball_Flash_Reverse.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, caster, false, caster, "SpinnigBottomRidge", castPos, caster, default, default, true, false, false, false, false);
                        SealSpellSlot(0, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                        SealSpellSlot(1, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                        SealSpellSlot(2, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                        SealSpellSlot(3, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                    }
                }
            }
            if(owner is Champion)
            {
                if(!this.ghostSpawned)
                {
                    if(owner.IsDead)
                    {
                        Vector3 missileEndPosition;
                        Minion other3;
                        caster = SetBuffCasterUnit();
                        missileEndPosition = GetUnitPosition(owner);
                        teamID = GetTeamID(attacker);
                        this.ghostSpawned = true;
                        other3 = SpawnMinion("TheDoomBall", "OriannaBall", "idle.lua", missileEndPosition, teamID ?? TeamId.TEAM_BLUE, false, true, false, true, true, true, 0, false, true, (Champion)caster);
                        AddBuff(attacker, other3, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        AddBuff(attacker, other3, new Buffs.OrianaGhostMinion(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
                    }
                }
            }
            if(caster.IsDead)
            {
                SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
            }
            else if(caster == owner)
            {
                AddBuff(caster, caster, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
            }
        }
        public override void OnDeath()
        {
            SpellEffectRemove(this.orianaPointer);
            if(!this.ghostSpawned)
            {
                if(!this.minionBall)
                {
                    ObjAIBase caster;
                    Vector3 missileEndPosition;
                    TeamId teamID;
                    Minion other3;
                    caster = SetBuffCasterUnit();
                    missileEndPosition = GetUnitPosition(owner);
                    teamID = GetTeamID(caster);
                    this.ghostSpawned = true;
                    other3 = SpawnMinion("TheDoomBall", "OriannaBall", "idle.lua", missileEndPosition, teamID ?? TeamId.TEAM_BLUE, false, true, false, true, true, true, 0, false, true, (Champion)caster);
                    AddBuff(caster, other3, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    AddBuff(caster, other3, new Buffs.OrianaGhostMinion(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
                }
            }
        }
    }
}