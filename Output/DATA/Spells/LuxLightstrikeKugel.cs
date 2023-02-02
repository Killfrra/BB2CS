#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LuxLightstrikeKugel : BBSpellScript
    {
        int[] effect0 = {9, 9, 9, 9, 9};
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            float nextBuffVars_LSCooldown;
            Vector3 nextBuffVars_Position;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_LSCooldown = this.effect0[level];
            nextBuffVars_Position = missileEndPosition;
            AddBuff(attacker, attacker, new Buffs.LuxLightstrikeKugel(nextBuffVars_Position, nextBuffVars_LSCooldown), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, attacker, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetSlotSpellCooldownTimeVer2(0, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}
namespace Buffs
{
    public class LuxLightstrikeKugel : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "root", },
            AutoBuffActivateEffect = new[]{ "LuxLightstrike_mis.troy", },
        };
        Vector3 position;
        float lSCooldown;
        Particle particle;
        Particle particle1;
        Particle particle2;
        Region bubbleID;
        Particle partExplode; // UNUSED
        int[] effect0 = {60, 105, 150, 195, 240};
        float[] effect1 = {-0.2f, -0.24f, -0.28f, -0.32f, -0.36f};
        public LuxLightstrikeKugel(Vector3 position = default, float lSCooldown = default)
        {
            this.position = position;
            this.lSCooldown = lSCooldown;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            Vector3 position;
            //RequireVar(this.position);
            //RequireVar(this.level);
            //RequireVar(this.lSCooldown);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LuxLightstrikeToggle));
            teamOfOwner = GetTeamID(owner);
            position = this.position;
            SpellEffectCreate(out this.particle, out _, "LuxLightstrike_mis.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 400, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, position, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particle1, out this.particle2, "LuxLightstrike_tar_green.troy", "LuxLightstrike_tar_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 400, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, "top", position, target, default, default, false, false, false, false, false);
            SealSpellSlot(2, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_CHAMPION);
            this.bubbleID = AddPosPerceptionBubble(teamOfOwner, 650, position, 6, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            float boomDamage;
            Vector3 position;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            TeamId casterID;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            boomDamage = this.effect0[level];
            position = this.position;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                Particle partExplodeHit; // UNUSED
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, boomDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                SpellEffectCreate(out partExplodeHit, out _, "globalhit_mana.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false, false, false, false, false);
                if(unit is not BaseTurret)
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.LuxIlluminatingFraulein(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle1);
            SpellEffectRemove(this.particle2);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LuxLightstrikeKugel));
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * this.lSCooldown;
            SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            casterID = GetTeamID(owner);
            if(casterID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.partExplode, out _, "LuxBlitz_nova.troy", default, TeamId.TEAM_BLUE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, position, owner, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.partExplode, out _, "LuxBlitz_nova.troy", default, TeamId.TEAM_PURPLE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, position, owner, default, default, true, false, false, false, false);
            }
            RemovePerceptionBubble(this.bubbleID);
        }
        public override void OnUpdateActions()
        {
            int level;
            float nextBuffVars_MoveSpeedMod;
            Vector3 position;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MoveSpeedMod = this.effect1[level];
            position = this.position;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff((ObjAIBase)owner, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 0.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}