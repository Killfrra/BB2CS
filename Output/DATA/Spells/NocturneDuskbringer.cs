#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneDuskbringer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "Nocturne_Duskbringer.dds",
            IsDeathRecapSource = true,
        };
        float hastePercent;
        int bonusAD;
        Particle particle2;
        Particle particle;
        float lastTimeExecuted;
        public NocturneDuskbringer(float hastePercent = default, int bonusAD = default)
        {
            this.hastePercent = hastePercent;
            this.bonusAD = bonusAD;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            Vector3 orientationPoint;
            int nocturneSkinID;
            Vector3 point1; // UNUSED
            SetInvulnerable(owner, true);
            SetGhosted(owner, true);
            SetStealthed(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
            SetNoRender(owner, true);
            //RequireVar(this.hastePercent);
            //RequireVar(this.bonusAD);
            teamOfOwner = GetTeamID(owner);
            orientationPoint = GetPointByUnitFacingOffset(owner, 10000, 0);
            nocturneSkinID = GetSkinID(attacker);
            if(nocturneSkinID == 1)
            {
                SpellEffectCreate(out this.particle2, out this.particle, "NocturneDuskbringer_path_frost_green.troy", "NocturneDuskbringer_path_frost_red.troy", teamOfOwner, 240, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, true, false, orientationPoint);
            }
            else if(nocturneSkinID == 4)
            {
                SpellEffectCreate(out this.particle2, out this.particle, "NocturneDuskbringer_path_ghost_green.troy", "NocturneDuskbringer_path_ghost_red.troy", teamOfOwner, 240, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, true, false, orientationPoint);
            }
            else
            {
                SpellEffectCreate(out this.particle2, out this.particle, "NocturneDuskbringer_path_green.troy", "NocturneDuskbringer_path_red.troy", teamOfOwner, 240, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, true, false, orientationPoint);
            }
            SetTargetable(owner, false);
            point1 = GetPointByUnitFacingOffset(owner, 0, 0);
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            SetTargetable(owner, true);
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetStealthed(owner, true);
            SetTargetable(owner, false);
        }
        public override void OnUpdateActions()
        {
            float distance;
            float nextBuffVars_HastePercent;
            float nextBuffVars_BonusAD;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                distance = DistanceBetweenObjects("Owner", "Attacker");
                if(distance <= 80)
                {
                    nextBuffVars_HastePercent = this.hastePercent;
                    nextBuffVars_BonusAD = this.bonusAD;
                    AddBuff(attacker, attacker, new Buffs.NocturneDuskbringerHaste(nextBuffVars_HastePercent, nextBuffVars_BonusAD), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
    }
}
namespace Spells
{
    public class NocturneDuskbringer : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {0.15f, 0.2f, 0.25f, 0.3f, 0.35f};
        int[] effect1 = {15, 25, 35, 45, 55};
        float[] effect2 = {0.15f, 0.2f, 0.25f, 0.3f, 0.35f};
        int[] effect3 = {15, 25, 35, 45, 55};
        float[] effect4 = {0.15f, 0.2f, 0.25f, 0.3f, 0.35f};
        int[] effect5 = {15, 25, 35, 45, 55};
        float[] effect6 = {48.75f, 86.25f, 123.75f, 161.25f, 198.75f};
        float[] effect7 = {48.75f, 86.25f, 123.75f, 161.25f, 198.75f};
        float[] effect8 = {48.75f, 86.25f, 123.75f, 161.25f, 198.75f};
        float[] effect9 = {48.75f, 86.25f, 123.75f, 161.25f, 198.75f};
        public override void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition)
        {
            float nextBuffVars_HastePercent;
            int nextBuffVars_BonusAD;
            TeamId teamID;
            Vector3 groundHeight;
            Minion other3;
            Vector3 targetPos;
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_HastePercent = this.effect0[level];
            nextBuffVars_BonusAD = this.effect1[level];
            teamID = GetTeamID(owner);
            groundHeight = GetGroundHeight(missilePosition);
            groundHeight = ModifyPosition(0, 10, 0);
            other3 = SpawnMinion("DarkPath", "testcube", "idle.lua", groundHeight, teamID, true, true, true, true, false, true, 0, false, true);
            targetPos = GetCastSpellTargetPos();
            FaceDirection(other3, targetPos);
            AddBuff((ObjAIBase)owner, other3, new Buffs.NocturneDuskbringer(nextBuffVars_HastePercent, nextBuffVars_BonusAD), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void SelfExecute()
        {
            float nextBuffVars_HastePercent;
            float nextBuffVars_BonusAD;
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_HastePercent = this.effect2[level];
            nextBuffVars_BonusAD = this.effect3[level];
            AddBuff(attacker, attacker, new Buffs.NocturneDuskbringerHaste(nextBuffVars_HastePercent, nextBuffVars_BonusAD), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nocturneSkinID;
            TeamId teamID;
            float nextBuffVars_HastePercent;
            int nextBuffVars_BonusAD;
            Vector3 nextBuffVars_LastPosition;
            Vector3 myPosition;
            Minion other3;
            Vector3 targetPos;
            TeamId teamID2; // UNUSED
            float physPreMod;
            float physPostMod;
            bool isStealthed;
            Vector3 lastPosition;
            Particle targettrail; // UNUSED
            bool canSee;
            nocturneSkinID = GetSkinID(owner);
            teamID = GetTeamID(owner);
            if(target == owner)
            {
                level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_HastePercent = this.effect4[level];
                nextBuffVars_BonusAD = this.effect5[level];
                teamID = GetTeamID(owner);
                myPosition = GetUnitPosition(owner);
                other3 = SpawnMinion("DarkPath", "testcube", "idle.lua", myPosition, teamID, true, true, true, true, false, true, 0, false, true);
                targetPos = GetCastSpellTargetPos();
                FaceDirection(other3, targetPos);
                AddBuff((ObjAIBase)owner, other3, new Buffs.NocturneDuskbringer(nextBuffVars_HastePercent, nextBuffVars_BonusAD), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                teamID2 = GetTeamID(target);
                physPreMod = GetFlatPhysicalDamageMod(owner);
                physPostMod = 0.75f * physPreMod;
                isStealthed = GetStealthed(target);
                if(!isStealthed)
                {
                    if(target is Champion)
                    {
                        BreakSpellShields(target);
                        lastPosition = GetPointByUnitFacingOffset(target, 2000, 0);
                        nextBuffVars_LastPosition = lastPosition;
                        AddBuff((ObjAIBase)owner, target, new Buffs.NocturneDuskbringerTrail(nextBuffVars_LastPosition), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        SpellEffectCreate(out targettrail, out _, "NocturneDuskbringer_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                        ApplyDamage(attacker, target, physPostMod + this.effect6[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                        if(nocturneSkinID == 1)
                        {
                            SpellEffectCreate(out targettrail, out _, "NocturneDuskbringer_frost_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, false, false, false, false, false);
                        }
                        else
                        {
                            SpellEffectCreate(out targettrail, out _, "NocturneDuskbringer_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, false, false, false, false, false);
                        }
                    }
                    else
                    {
                        BreakSpellShields(target);
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        SpellEffectCreate(out targettrail, out _, "NocturneDuskbringer_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                        ApplyDamage(attacker, target, physPostMod + this.effect7[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                    }
                }
                else
                {
                    if(target is Champion)
                    {
                        BreakSpellShields(target);
                        lastPosition = GetPointByUnitFacingOffset(target, 2000, 0);
                        nextBuffVars_LastPosition = lastPosition;
                        AddBuff((ObjAIBase)owner, target, new Buffs.NocturneDuskbringerTrail(nextBuffVars_LastPosition), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        SpellEffectCreate(out targettrail, out _, "NocturneDuskbringer_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                        SpellEffectCreate(out targettrail, out _, "NocturneDuskbringer_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, false, false, false, false, false);
                        ApplyDamage(attacker, target, physPostMod + this.effect8[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                        if(nocturneSkinID == 1)
                        {
                            SpellEffectCreate(out targettrail, out _, "NocturneDuskbringer_frost_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, false, false, false, false, false);
                        }
                        else
                        {
                            SpellEffectCreate(out targettrail, out _, "NocturneDuskbringer_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, false, false, false, false, false);
                        }
                    }
                    else
                    {
                        canSee = CanSeeTarget(owner, target);
                        if(canSee)
                        {
                            BreakSpellShields(target);
                            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            SpellEffectCreate(out targettrail, out _, "NocturneDuskbringer_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                            ApplyDamage(attacker, target, physPostMod + this.effect9[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                        }
                    }
                }
            }
        }
    }
}