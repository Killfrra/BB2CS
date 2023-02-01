#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pantheon_Aegis : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pantheon Aegis",
            BuffTextureName = "Pantheon_AOZ.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int aegisCounter; // UNUSED
        public override void OnActivate()
        {
            this.aegisCounter = 0;
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int count;
            if(hitResult != HitResult.HIT_Miss)
            {
                if(hitResult != HitResult.HIT_Dodge)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield2)) == 0)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield)) == 0)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_Aegis_Counter(), 5, 1, 25000, BuffAddType.STACKS_AND_OVERLAPS, BuffType.AURA, 0, false, false, false);
                            count = GetBuffCountFromAll(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                            if(count >= 4)
                            {
                                AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_AegisShield(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                                SpellBuffClear(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                            }
                        }
                    }
                }
            }
        }
    }
}