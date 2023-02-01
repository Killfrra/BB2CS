#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearWDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VayneSilverDebuff",
            BuffTextureName = "Vayne_SilveredBolts.dds",
        };
        bool ready;
        bool critical; // UNUSED
        Particle particle1;
        public override void OnActivate()
        {
            float cD;
            int count;
            float healthPercent; // UNUSED
            this.ready = false;
            this.critical = false;
            cD = GetSlotSpellCooldownTime(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(cD <= 0)
            {
                count = GetBuffCountFromAll(attacker, nameof(Buffs.VolibearWStats));
                if(count == 4)
                {
                    this.ready = true;
                    SpellEffectCreate(out this.particle1, out _, "Volibear_tar_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, target, default, default, false, false, false, false, false);
                    healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if(this.ready)
            {
                SpellEffectRemove(this.particle1);
            }
        }
        public override void OnUpdateActions()
        {
            bool readyNew;
            bool criticalNew; // UNUSED
            float cD;
            int count;
            float distance;
            readyNew = false;
            criticalNew = false;
            cD = GetSlotSpellCooldownTime(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(cD <= 0)
            {
                count = GetBuffCountFromAll(attacker, nameof(Buffs.VolibearWStats));
                if(count == 4)
                {
                    distance = DistanceBetweenObjects("Attacker", "Owner");
                    if(distance <= 350)
                    {
                        readyNew = true;
                    }
                }
            }
            if(readyNew)
            {
                if(!this.ready)
                {
                    this.ready = true;
                    SpellEffectCreate(out this.particle1, out _, "Volibear_tar_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, target, default, default, false, false, false, false, false);
                }
            }
            if(!readyNew)
            {
                if(this.ready)
                {
                    this.ready = false;
                    SpellEffectRemove(this.particle1);
                }
            }
        }
    }
}