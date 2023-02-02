#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleCarpetBombBurnDest : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GatlingGunSelf",
            BuffTextureName = "Corki_GatlingGun.dds",
            SpellToggleSlot = 3,
        };
        float lastTimeExecuted;
        float burnDmg;
        int[] effect0 = {50, 70, 90};
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                int level;
                level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.burnDmg = this.effect0[level];
                ApplyDamage(attacker, owner, this.burnDmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.1f, 0, false, false, attacker);
            }
        }
    }
}