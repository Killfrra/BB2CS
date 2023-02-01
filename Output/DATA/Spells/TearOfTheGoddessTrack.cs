#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TearOfTheGoddessTrack : BBBuffScript
    {
        float cooldownResevoir;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            this.cooldownResevoir = 0;
        }
        public override void OnUpdateStats()
        {
            IncFlatPARPoolMod(owner, charVars.TearBonusMana, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(3, ref this.lastTimeExecuted, true))
            {
                if(this.cooldownResevoir < 2)
                {
                    this.cooldownResevoir++;
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            Particle killMe_; // UNUSED
            if(spellVars.DoesntTriggerSpellCasts)
            {
            }
            else
            {
                if(this.cooldownResevoir > 0)
                {
                    SpellEffectCreate(out killMe_, out _, "TearoftheGoddess_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    charVars.TearBonusMana += 4;
                    charVars.TearBonusMana = Math.Min(charVars.TearBonusMana, 1000);
                    this.cooldownResevoir += -1;
                }
            }
        }
    }
}