#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HeimerdingerTurretDetonation : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnDeath()
        {
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickReviveAllySelf)) == 0)
            {
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRADelay)) == 0)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 20000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions, default, true))
                    {
                        if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.H28GEvolutionTurret)) > 0)
                        {
                            ApplyDamage(attacker, unit, 1000, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_RAW, 1, 1, 0, false, false, attacker);
                        }
                    }
                }
            }
        }
    }
}