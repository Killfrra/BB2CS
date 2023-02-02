#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class WormAttack : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            baseDamage = GetBaseAttackDamage(owner);
            ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 0.7f, 0, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.WormAttack(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class WormAttack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WormAttack",
            BuffTextureName = "48thSlave_SoulDrain.dds",
        };
        float damageMod;
        public override void OnActivate()
        {
            float charDamage;
            charDamage = GetTotalAttackDamage(owner);
            this.damageMod = charDamage * -0.5f;
            IncFlatPhysicalDamageMod(owner, this.damageMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.damageMod);
        }
    }
}