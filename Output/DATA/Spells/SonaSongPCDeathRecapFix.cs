#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SonaSongPCDeathRecapFix : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int levelDamage;
            float bonusDamage;
            float totalDamage;
            float attackDamage;
            levelDamage = GetLevel(owner);
            bonusDamage = levelDamage * 9;
            totalDamage = bonusDamage + 14;
            attackDamage = GetTotalAttackDamage(owner);
            ApplyDamage((ObjAIBase)owner, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            AddBuff(attacker, attacker, new Buffs.IfHasBuffCheck(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    float nextBuffVars_MoveSpeedMod;
                    BreakSpellShields(target);
                    nextBuffVars_MoveSpeedMod = -0.4f;
                    AddBuff((ObjAIBase)owner, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
            ApplyDamage((ObjAIBase)owner, target, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
            SpellBuffRemove(owner, nameof(Buffs.SonaPowerChord), (ObjAIBase)owner, 0);
        }
    }
}
namespace Buffs
{
    public class SonaSongPCDeathRecapFix : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
        float totalDamage;
        public SonaSongPCDeathRecapFix(float totalDamage = default)
        {
            this.totalDamage = totalDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.totalDamage);
            ApplyDamage(attacker, owner, this.totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
        }
    }
}