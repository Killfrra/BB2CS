#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class Sheen : BBCharScript
    {
        public override void OnLaunchAttack()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.SheenDelay(), 1, 1, 1.4f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            }
        }
    }
}
namespace Buffs
{
    public class Sheen : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[]{ "enrage_buf.troy", "enrage_buf.troy", },
            BuffName = "Sheen",
            BuffTextureName = "3057_Sheen.dds",
        };
        float baseDamage;
        bool isSheen;
        public Sheen(float baseDamage = default, bool isSheen = default)
        {
            this.baseDamage = baseDamage;
            this.isSheen = isSheen;
        }
        public override void OnActivate()
        {
            //RequireVar(this.baseDamage);
            //RequireVar(this.isSheen);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    float percentBase;
                    if(!this.isSheen)
                    {
                        percentBase = this.baseDamage * 1.5f;
                        damageAmount += percentBase;
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.SheenDelay(), 1, 1, 1.3f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                        }
                        SpellBuffClear(owner, nameof(Buffs.Sheen));
                        SpellBuffRemoveCurrent(owner);
                    }
                    if(this.isSheen)
                    {
                        percentBase = this.baseDamage * 1;
                        damageAmount += percentBase;
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.SheenDelay(), 1, 1, 1.2f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                        }
                        SpellBuffClear(owner, nameof(Buffs.Sheen));
                        SpellBuffRemoveCurrent(owner);
                    }
                }
            }
        }
    }
}