#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FlashFrost : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.FlashFrost)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.FlashFrost), (ObjAIBase)owner);
            }
            else
            {
                Vector3 targetPos;
                level = GetCastSpellLevelPlusOne();
                targetPos = GetCastSpellTargetPos();
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.285f);
                AddBuff(attacker, target, new Buffs.FlashFrost(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class FlashFrost : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Flash Frost",
            BuffTextureName = "Cryophoenix_FrigidOrb.dds",
            SpellToggleSlot = 1,
        };
        int missileAlive;
        SpellMissile flashMissileId;
        int[] effect0 = {60, 90, 120, 150, 180};
        int[] effect1 = {12, 11, 10, 9, 8};
        int[] effect2 = {60, 90, 120, 150, 180};
        public override void OnActivate()
        {
            SetTargetingType(0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Self, owner);
            this.missileAlive = 0;
            this.flashMissileId = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            float cooldownPerLevel;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            SetTargetingType(0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Location, owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(this.missileAlive == 1)
            {
                Vector3 missileEndPosition;
                TeamId teamID;
                Particle a; // UNUSED
                missileEndPosition = GetMissilePosFromID(this.flashMissileId ?? 0);
                DestroyMissile(this.flashMissileId);
                teamID = GetTeamID(attacker);
                if(teamID == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out a, out _, "cryo_FlashFrost_tar.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, missileEndPosition, target, default, default, true);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "cryo_FlashFrost_tar.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, missileEndPosition, target, default, default, true);
                }
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, missileEndPosition, 210, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_MovementSpeedMod;
                    float nextBuffVars_AttackSpeedMod;
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
                    ApplyStun(attacker, unit, 0.75f);
                    nextBuffVars_MovementSpeedMod = -0.2f;
                    nextBuffVars_AttackSpeedMod = 0;
                    AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
                this.missileAlive = 0;
            }
            cooldownPerLevel = this.effect1[level];
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * cooldownPerLevel;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            if(spellName == nameof(Spells.FlashFrostSpell))
            {
                TeamId teamOfOwner;
                int level;
                Particle arr; // UNUSED
                teamOfOwner = GetTeamID(owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellEffectCreate(out arr, out _, "cryo_FlashFrost_tar.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, missileEndPosition, target, default, default, true);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, missileEndPosition, 230, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_MovementSpeedMod;
                    float nextBuffVars_AttackSpeedMod;
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
                    ApplyStun(attacker, unit, 1);
                    nextBuffVars_MovementSpeedMod = -0.2f;
                    nextBuffVars_AttackSpeedMod = 0;
                    AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
                this.missileAlive = 0;
                SpellBuffRemove(owner, nameof(Buffs.FlashFrost), (ObjAIBase)owner);
            }
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            if(this.flashMissileId == 0)
            {
                SpellMissile missileId; // UNITIALIZED
                this.flashMissileId = missileId;
                this.missileAlive = 1;
            }
        }
    }
}