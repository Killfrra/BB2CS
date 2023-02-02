#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BloodScent : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 45f, 40f, 35f, 30f, 25f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.BloodScent_internal)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.BloodScent_internal), (ObjAIBase)owner, 0);
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.BloodScent_internal(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class BloodScent : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", "", },
            BuffName = "Haste",
            BuffTextureName = "Wolfman_Bloodscent.dds",
            SpellVOOverrideSkins = new[]{ "HyenaWarwick", },
        };
        float moveSpeedBuff;
        Particle part1;
        Particle part3;
        Particle part2;
        Particle part4;
        float[] effect0 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        public BloodScent(float moveSpeedBuff = default)
        {
            this.moveSpeedBuff = moveSpeedBuff;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            int ownerSkinID;
            teamID = GetTeamID(owner);
            //RequireVar(this.moveSpeedBuff);
            SpellEffectCreate(out this.part1, out _, "wolfman_bloodscent_activate_speed.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.part3, out _, "wolfman_bloodscent_activate_blood_buff.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.part2, out _, "wolfman_bloodscent_activate_blood_buff.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.part4, out _, "wolfman_bloodscent_activate_blood_buff_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, false, false, false, false);
            ownerSkinID = GetSkinID(owner);
            if(ownerSkinID == 7)
            {
                OverrideAnimation("Run", "Run2", owner);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            int ownerSkinID;
            SpellEffectRemove(this.part1);
            SpellEffectRemove(this.part2);
            SpellEffectRemove(this.part3);
            SpellEffectRemove(this.part4);
            ownerSkinID = GetSkinID(owner);
            if(ownerSkinID == 7)
            {
                StopCurrentOverrideAnimation("Run", owner, false);
                OverrideAnimation("Run", "Run", owner);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(attacker, this.moveSpeedBuff);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 2)
            {
                int level;
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.moveSpeedBuff = this.effect0[level];
            }
        }
    }
}