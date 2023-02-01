#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AlphaStrikeTarget : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "AlphaStrike_prison.troy", },
            BuffName = "Alpha Strike",
            BuffTextureName = "MasterYi_LeapStrike.dds",
        };
        float baseDamage;
        float chanceToKill;
        public AlphaStrikeTarget(float baseDamage = default, float chanceToKill = default)
        {
            this.baseDamage = baseDamage;
            this.chanceToKill = chanceToKill;
        }
        public override void OnActivate()
        {
            //RequireVar(this.baseDamage);
            //RequireVar(this.chanceToKill);
        }
        public override void OnUpdateActions()
        {
            int count;
            count = GetBuffCountFromAll(attacker, nameof(Buffs.AlphaStrike));
            if(count == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            Particle a; // UNUSED
            float bonusDamage;
            SpellEffectCreate(out a, out _, "AlphaStrike_Slash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            if(owner is Champion)
            {
                ApplyDamage(attacker, owner, this.baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, default, false, false);
            }
            else
            {
                if(RandomChance() < this.chanceToKill)
                {
                    bonusDamage = this.baseDamage + 400;
                    ApplyDamage(attacker, owner, bonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, default, false, false);
                }
                else
                {
                    ApplyDamage(attacker, owner, this.baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, default, false, false);
                }
            }
        }
    }
}