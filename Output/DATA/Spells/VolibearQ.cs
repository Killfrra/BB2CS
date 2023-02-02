#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VolibearQ : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {12, 11, 10, 9, 8};
        float[] effect1 = {0.45f, 0.45f, 0.45f, 0.45f, 0.45f};
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown;
            float nextBuffVars_SpeedMod;
            nextBuffVars_SpellCooldown = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearQ(nextBuffVars_SpellCooldown), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            nextBuffVars_SpeedMod = this.effect1[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearQSpeed(nextBuffVars_SpeedMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class VolibearQ : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "VolibearQ",
            BuffTextureName = "VolibearQ.dds",
        };
        Particle c;
        Particle a;
        Particle b;
        Particle particleID;
        Particle particleID2;
        Particle particleID3;
        float spellCooldown;
        public VolibearQ(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.c, out _, "Volibear_Q_cas_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.a, out _, "volibear_Q_attack_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_BUFFBONE_GLB_HAND_LOC", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.b, out _, "volibear_Q_attack_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_BUFFBONE_GLB_HAND_LOC", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particleID, out _, "volibear_Q_lightning_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_forearm", default, owner, "l_middle_finger", default, false, false, false, false, false);
            SpellEffectCreate(out this.particleID2, out _, "volibear_Q_lightning_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_forearm", default, owner, "r_middle_finger", default, false, false, false, false, false);
            SpellEffectCreate(out this.particleID3, out _, "volibear_Q_lightning_cast_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_uparm", default, owner, "r_uparm", default, false, false, false, false, false);
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, true);
            CancelAutoAttack(owner, true);
            OverrideAnimation("Idle1", "Spell1_Idle", owner);
            OverrideAnimation("Idle2", "Spell1_Idle", owner);
            OverrideAnimation("Idle3", "Spell1_Idle", owner);
            OverrideAnimation("Idle4", "Spell1_Idle", owner);
            OverrideAnimation("Run", "Spell1_Run", owner);
            OverrideAnimation("Spell4", "Spell1_Idle", owner);
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            spellCooldown = this.spellCooldown;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellEffectRemove(this.a);
            SpellEffectRemove(this.b);
            SpellEffectRemove(this.c);
            SpellEffectRemove(this.particleID);
            SpellEffectRemove(this.particleID2);
            SpellEffectRemove(this.particleID3);
            ClearOverrideAnimation("Idle1", owner);
            ClearOverrideAnimation("Idle2", owner);
            ClearOverrideAnimation("Idle3", owner);
            ClearOverrideAnimation("Idle4", owner);
            ClearOverrideAnimation("Run", owner);
            ClearOverrideAnimation("Spell4", owner);
            RemoveOverrideAutoAttack(owner, false);
        }
        public override void OnPreAttack()
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    float offset;
                    offset = GetOffsetAngle(target, attacker.Position);
                    charVars.BouncePos = GetPointByUnitFacingOffset(target, 400, offset);
                }
            }
        }
    }
}