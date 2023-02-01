#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SummonerBoostSpellShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "SpellBlock_eff.troy", },
            BuffName = "Spell Shield",
            BuffTextureName = "Summoner_boost.dds",
        };
        bool willRemove;
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(this.willRemove)
                {
                    returnValue = false;
                }
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void OnUpdateStats()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(this.willRemove)
            {
                if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
                {
                    damageAmount = 0;
                }
            }
        }
        public override void OnBeingSpellHit(SpellScriptMetaData spellVars)
        {
            Particle ar; // UNUSED
            if(!spellVars.DoesntTriggerSpellCasts)
            {
                if(owner.Team != attacker.Team)
                {
                    this.willRemove = true;
                    SpellEffectCreate(out ar, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false);
                }
            }
        }
    }
}