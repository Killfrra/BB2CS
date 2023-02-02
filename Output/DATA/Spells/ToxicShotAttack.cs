#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ToxicShotAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float attackDamage;
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.Toxicshotapplicator(), 1, 1, 0.1f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            attackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class ToxicShotAttack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Toxic Shot",
            BuffTextureName = "Teemo_PoisonedDart.dds",
            SpellFXOverrideSkins = new[]{ "AstronautTeemo", },
        };
    }
}