#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliShadowDanceCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateActions()
        {
            if(!owner.IsDead)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliShadowDanceTimer)) == 0)
                {
                    int count;
                    count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliShadowDance));
                    if(count != 3)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDanceTimer(), 1, 1, charVars.DanceTimerCooldown, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    }
                }
            }
        }
        public override void OnKill()
        {
            if(target is Champion)
            {
                int count;
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
                count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliShadowDance));
                if(count == 3)
                {
                    SpellBuffClear(owner, nameof(Buffs.AkaliShadowDanceTimer));
                }
            }
        }
        public override void OnAssist()
        {
            int count;
            AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliShadowDance));
            if(count != 3)
            {
                SpellBuffClear(owner, nameof(Buffs.AkaliShadowDanceTimer));
            }
        }
    }
}