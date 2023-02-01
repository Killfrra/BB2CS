#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GarenRecouperateOn : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "GarenRecouperateOn",
            BuffTextureName = "Garen_Perseverance.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            float maxHealth; // UNUSED
            float curHealth; // UNUSED
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.GarenRecoupDebuff)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecouperate1(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, false, false, false);
                }
            }
        }
    }
}