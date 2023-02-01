#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VeigarBalefulStrike : BBBuffScript
    {
        object bonusAP;
        int[] effect0 = {9999, 9999, 9999, 9999, 9999};
        public VeigarBalefulStrike(object bonusAP = default)
        {
            this.bonusAP = bonusAP;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusAP);
            //RequireVar(this.maxBonus);
        }
        public override void OnDeactivate(bool expired)
        {
            Particle placeholder; // UNUSED
            if(attacker.IsDead)
            {
                SpellEffectCreate(out placeholder, out _, "permission_ability_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnKill()
        {
            int level; // UNUSED
            float bonusAdd;
            if(charVars.TotalBonus < charVars.MaxBonus)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                bonusAdd = 1 + charVars.TotalBonus;
                charVars.TotalBonus = bonusAdd;
                IncPermanentFlatMagicDamageMod(owner, 1);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            int level;
            object nextBuffVars_BonusAP;
            float nextBuffVars_MaxBonus;
            if(slot == 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_BonusAP = this.bonusAP;
                charVars.MaxBonus = this.effect0[level];
                nextBuffVars_MaxBonus = charVars.MaxBonus;
            }
        }
    }
}
namespace Spells
{
    public class VeigarBalefulStrike : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 8f, 7f, 6f, 5f, 4f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {80, 125, 170, 215, 260};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(!target.IsDead)
            {
                AddBuff((ObjAIBase)target, owner, new Buffs.VeigarBalefulStrike(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
        }
    }
}