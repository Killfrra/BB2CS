#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AlZaharVoidlingDetonation : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnDeath()
        {
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 20000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions, default, true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.AlZaharVoidling)) > 0)
                {
                    ApplyDamage(attacker, unit, 2000, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_RAW, 1, 1, 1, false, false);
                }
            }
        }
    }
}