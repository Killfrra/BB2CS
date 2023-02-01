#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneParanoiaTargeting : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "NocturneParanoiaTarget",
            BuffTextureName = "Nocturne_Paranoia.dds",
        };
        bool partCreated;
        int range;
        Particle tpar;
        int[] effect0 = {2000, 2750, 3500};
        public override void OnActivate()
        {
            int level;
            float distance;
            this.partCreated = false;
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.range = this.effect0[level];
            distance = DistanceBetweenObjects("Owner", "Attacker");
            if(distance <= this.range)
            {
                SpellEffectCreate(out this.tpar, out _, "NocturneParanoiaTargeting.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, owner, default, default, false, default, default, false);
                this.partCreated = true;
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if(this.partCreated)
            {
                SpellEffectRemove(this.tpar);
            }
        }
        public override void OnUpdateActions()
        {
            float distance;
            distance = DistanceBetweenObjects("Owner", "Attacker");
            if(this.partCreated)
            {
                if(distance > this.range)
                {
                    SpellEffectRemove(this.tpar);
                    this.partCreated = false;
                }
            }
            else
            {
                if(distance <= this.range)
                {
                    SpellEffectCreate(out this.tpar, out _, "NocturneParanoiaTargeting.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, owner, default, default, false, default, default, false);
                    this.partCreated = true;
                }
            }
        }
    }
}