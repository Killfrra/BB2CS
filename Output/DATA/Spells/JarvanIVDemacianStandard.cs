#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JarvanIVDemacianStandard : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "JarvanIVDemacianStandard",
            BuffTextureName = "JarvanIV_DemacianStandard.dds",
        };
        int armorMod;
        float attackSpeedMod;
        int count; // UNUSED
        Particle particle;
        Particle particle2;
        float lastTimeExecuted;
        public JarvanIVDemacianStandard(int armorMod = default, float attackSpeedMod = default)
        {
            this.armorMod = armorMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            IncPermanentFlatBubbleRadiusMod(owner, -500);
            IncPercentBubbleRadiusMod(owner, -0.5f);
            //RequireVar(this.armorMod);
            //RequireVar(this.attackSpeedMod);
            this.count = 0;
            SetSuppressCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetIgnoreCallForHelp(owner, true);
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out this.particle, out this.particle2, "JarvanDemacianStandard_buf_green.troy", "JarvanDemacianStandard_buf_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SetNotTargetableToTeam(owner, false, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            AddBuff((ObjAIBase)owner, owner, new Buffs.NoRenderExpirationBuff(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_AttackSpeedMod;
            float nextBuffVars_ArmorMod;
            nextBuffVars_AttackSpeedMod = this.attackSpeedMod;
            nextBuffVars_ArmorMod = this.armorMod;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff(attacker, unit, new Buffs.JarvanIVDemacianStandardBuff(nextBuffVars_AttackSpeedMod, nextBuffVars_ArmorMod), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(attacker is BaseTurret)
            {
            }
            else
            {
                if(damageType == DamageType.DAMAGE_TYPE_TRUE)
                {
                    damageAmount = 0;
                }
                else
                {
                    damageAmount = 1;
                }
            }
        }
    }
}
namespace Spells
{
    public class JarvanIVDemacianStandard : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastTime = 0.115f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {60, 105, 150, 195, 240};
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            Particle a; // UNUSED
            Minion other3;
            int nextBuffVars_Level;
            float nextBuffVars_DamageToDeal;
            float baseDamage;
            float abilityPower;
            float abilityPowerPostMod;
            float damageToDeal;
            teamOfOwner = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            SpellEffectCreate(out a, out _, "JarvanDemacianStandard_mis.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "R_Hand", default, attacker, default, default, true, default, default, false, false);
            other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, false, 0, false, false, (Champion)owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_Level = level;
            baseDamage = this.effect0[level];
            abilityPower = GetFlatMagicDamageMod(owner);
            abilityPowerPostMod = 0.8f * abilityPower;
            damageToDeal = abilityPowerPostMod + baseDamage;
            nextBuffVars_DamageToDeal = damageToDeal;
            AddBuff(attacker, other3, new Buffs.JarvanIVDemacianStandardDelay(nextBuffVars_Level, nextBuffVars_DamageToDeal), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            FaceDirection(owner, targetPos);
            PlayAnimation("Spell3", 0.75f, owner, false, true, false);
        }
    }
}