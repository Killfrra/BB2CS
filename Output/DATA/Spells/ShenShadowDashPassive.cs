#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShenShadowDashPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float cD;
            float newCD;
            if(attacker is ObjAIBase)
            {
                attacker = SetBuffCasterUnit();
                if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.ShenShadowDashCooldown)) == 0)
                {
                    AddBuff(attacker, attacker, new Buffs.ShenShadowDashCooldown(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
                    cD = GetSlotSpellCooldownTime(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cD > 0)
                    {
                        newCD = cD - 1;
                        SetSlotSpellCooldownTimeVer2(newCD, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                    }
                }
            }
        }
    }
}