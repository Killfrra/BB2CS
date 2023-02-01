#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DefensiveBallCurl : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "DefensiveBallCurl",
            BuffTextureName = "Armordillo_ShellBash.dds",
        };
        float armorAmount;
        float damageReturn;
        int casterID;
        Particle particle;
        public DefensiveBallCurl(float armorAmount = default, float damageReturn = default)
        {
            this.armorAmount = armorAmount;
            this.damageReturn = damageReturn;
        }
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PowerBall)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.PowerBall), (ObjAIBase)owner, 0);
            }
            //RequireVar(this.armorAmount);
            //RequireVar(this.damageReturn);
            this.casterID = PushCharacterData("RammusDBC", owner, false);
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.DefensiveBallCurlCancel));
            SpellEffectCreate(out this.particle, out _, "DefensiveBallCurl_buf", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SetSlotSpellCooldownTimeVer2(1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float baseCD;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            float finalCooldown;
            PopCharacterData(owner, this.casterID);
            SpellEffectCreate(out _, out _, "DBC_out.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            baseCD = 14;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * baseCD;
            finalCooldown = newCooldown - lifeTime;
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.DefensiveBallCurl));
            SetSlotSpellCooldownTimeVer2(finalCooldown, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SpellEffectRemove(this.particle);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.armorAmount);
            IncFlatSpellBlockMod(owner, this.armorAmount);
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float baseArmor;
            float armorMod;
            float damageReturn;
            if(attacker is BaseTurret)
            {
            }
            else
            {
                baseArmor = GetArmor(owner);
                armorMod = baseArmor * 0.1f;
                damageReturn = armorMod + this.damageReturn;
                ApplyDamage((ObjAIBase)owner, attacker, damageReturn, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, (ObjAIBase)owner);
                SpellEffectCreate(out _, out _, "Thornmail_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, false, false, false, false);
            }
        }
    }
}
namespace Spells
{
    public class DefensiveBallCurl : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {50, 75, 100, 125, 150};
        int[] effect1 = {15, 25, 35, 45, 55};
        public override void SelfExecute()
        {
            float nextBuffVars_ArmorAmount;
            float nextBuffVars_DamageReturn;
            nextBuffVars_ArmorAmount = this.effect0[level];
            nextBuffVars_DamageReturn = this.effect1[level];
            AddBuff(attacker, owner, new Buffs.DefensiveBallCurl(nextBuffVars_ArmorAmount, nextBuffVars_DamageReturn), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}