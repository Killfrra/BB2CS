#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TeemoMoveQuickPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float debuffDuration;
        public override void OnActivate()
        {
            this.debuffDuration = 5;
            AddBuff(attacker, owner, new Buffs.TeemoMoveQuickDebuff(), 1, 1, this.debuffDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.TeemoMoveQuickDebuff)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.TeemoMoveQuickSpeed));
            }
            else
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MoveQuick)) == 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.TeemoMoveQuickSpeed)) == 0)
                    {
                        AddBuff(attacker, target, new Buffs.TeemoMoveQuickSpeed(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageSource != default)
            {
                if(attacker is Champion)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.TeemoMoveQuickDebuff(), 1, 1, this.debuffDuration, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(attacker is BaseTurret)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.TeemoMoveQuickDebuff(), 1, 1, this.debuffDuration, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}