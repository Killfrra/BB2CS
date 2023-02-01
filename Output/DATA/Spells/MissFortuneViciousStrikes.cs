#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MissFortuneViciousStrikes : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "MissFortuneViciousStrikes",
            BuffTextureName = "MissFortune_ImpureShots.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float aSMod;
        Particle ar; // UNUSED
        public MissFortuneViciousStrikes(float aSMod = default)
        {
            this.aSMod = aSMod;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            int level; // UNUSED
            teamID = GetTeamID(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //RequireVar(this.aSMod);
            SpellEffectCreate(out this.ar, out _, "missFortune_viciousShots_attack_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_BUFFBONE_GLB_HAND_LOC", default, owner, default, default, true, default, default, false);
            SpellEffectCreate(out this.ar, out _, "missFortune_viciousShots_attack_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_BUFFBONE_GLB_HAND_LOC", default, owner, default, default, true, default, default, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.aSMod);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int level; // UNUSED
            if(target is ObjAIBase)
            {
                if(attacker.Team != target.Team)
                {
                    level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
        }
    }
}
namespace Spells
{
    public class MissFortuneViciousStrikes : BBSpellScript
    {
        float[] effect0 = {0.3f, 0.35f, 0.4f, 0.45f, 0.5f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_ASMod;
            nextBuffVars_ASMod = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneViciousStrikes(nextBuffVars_ASMod), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}