#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UdyrTigerStance : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrTigerStance",
            BuffTextureName = "Udyr_TigerStance.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 1,
        };
        int casterID; // UNUSED
        Particle tiger;
        float passiveAttackSpeed;
        int[] effect0 = {40, 90, 140, 190, 240};
        float[] effect1 = {0.15f, 0.2f, 0.25f, 0.3f, 0.35f};
        public UdyrTigerStance(float passiveAttackSpeed = default)
        {
            this.passiveAttackSpeed = passiveAttackSpeed;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            this.casterID = PushCharacterData("UdyrTiger", owner, false);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.tiger, out _, "tigerpelt.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, true, default, default, false, false);
            OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, true);
            //RequireVar(this.passiveAttackSpeed);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.tiger);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTigerPunch)) == 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTigerShred), (ObjAIBase)owner, 0);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.passiveAttackSpeed);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            TeamId teamID; // UNUSED
            int level;
            float baseDamage;
            float tAD;
            float dotDamage;
            float nextBuffVars_DotDamage;
            if(charVars.HitOnce)
            {
                charVars.HitOnce = false;
                teamID = GetTeamID(owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                baseDamage = this.effect0[level];
                tAD = GetTotalAttackDamage(owner);
                dotDamage = tAD * 1.7f;
                dotDamage += baseDamage;
                dotDamage *= 0.25f;
                nextBuffVars_DotDamage = dotDamage;
                if(target is ObjAIBase)
                {
                    if(target is not BaseTurret)
                    {
                        AddBuff(attacker, target, new Buffs.UdyrTigerPunchBleed(nextBuffVars_DotDamage), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                    }
                }
                SpellEffectRemove(charVars.Lhand);
                SpellEffectRemove(charVars.Rhand);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            int level;
            if(slot == 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.passiveAttackSpeed = this.effect1[level];
            }
        }
    }
}
namespace Spells
{
    public class UdyrTigerStance : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.15f, 0.2f, 0.25f, 0.3f, 0.35f};
        float[] effect1 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        public override void SelfExecute()
        {
            float cooldownPerc;
            float currentCD;
            float nextBuffVars_activeAttackSpeed;
            float nextBuffVars_passiveAttackSpeed;
            TeamId teamID;
            Particle tigerparticle; // UNUSED
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrBearStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrBearStance), (ObjAIBase)owner, 0);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrPhoenixStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrPhoenixStance), (ObjAIBase)owner, 0);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTurtleStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTurtleStance), (ObjAIBase)owner, 0);
            }
            cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= 1.5f;
            currentCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            nextBuffVars_activeAttackSpeed = this.effect0[level];
            nextBuffVars_passiveAttackSpeed = this.effect1[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.UdyrTigerStance(nextBuffVars_passiveAttackSpeed), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UdyrTigerPunch(nextBuffVars_activeAttackSpeed), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out tigerparticle, out _, "TigerStance.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UdyrTigerShred(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}