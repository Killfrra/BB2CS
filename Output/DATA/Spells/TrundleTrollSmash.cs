#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TrundleTrollSmash : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "TrundleTrollSmash",
            BuffTextureName = "Trundle_Bite.dds",
        };
        Particle geeves1;
        float spellCooldown;
        public TrundleTrollSmash(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.geeves1, out _, "Trundle_TrollSmash_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_Mouth", default, owner, default, default, true, default, default, false);
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            CancelAutoAttack(owner, true);
            UnlockAnimation(owner, true);
            SetDodgePiercing(owner, true);
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
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellEffectRemove(this.geeves1);
            SetDodgePiercing(owner, false);
        }
        public override void OnPreAttack()
        {
            int level;
            Vector3 targetPos;
            if(target is not BaseTurret)
            {
                if(target is ObjAIBase)
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    targetPos = GetUnitPosition(target);
                    FaceDirection(owner, targetPos);
                    SkipNextAutoAttack(owner);
                    SpellCast((ObjAIBase)owner, target, default, default, 0, SpellSlotType.ExtraSlots, level, false, false, false, false, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.TrundleTrollSmash), (ObjAIBase)owner);
                }
            }
        }
    }
}
namespace Spells
{
    public class TrundleTrollSmash : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {4, 4, 4, 4, 4};
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown;
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            nextBuffVars_SpellCooldown = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.TrundleTrollSmash(nextBuffVars_SpellCooldown), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}