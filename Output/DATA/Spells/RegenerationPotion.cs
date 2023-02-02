#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RegenerationPotion : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            float percentHealth;
            percentHealth = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
            if(percentHealth > 0.99f)
            {
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff((ObjAIBase)target, target, new Buffs.RegenerationPotion(), 5, 1, 15, BuffAddType.STACKS_AND_CONTINUE, BuffType.HEAL, 0, false, false, false);
            AddBuff((ObjAIBase)target, target, new Buffs.Potion_Internal(), 1, 1, 15, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.CountHealthPotion = GetBuffCountFromAll(owner, nameof(Buffs.RegenerationPotion));
        }
    }
}
namespace Buffs
{
    public class RegenerationPotion : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Regenerationpotion_itm.troy", },
            BuffName = "Health Potion",
            BuffTextureName = "2003_Regeneration_Potion.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            if(charVars.CountHealthPotion >= 2)
            {
                float stacksToAdd;
                stacksToAdd = charVars.CountHealthPotion - 1;
                AddBuff((ObjAIBase)owner, owner, new Buffs.RegenerationPotion(), 5, stacksToAdd, 15, BuffAddType.STACKS_AND_RENEWS, BuffType.HEAL, 0, true, false, false);
                AddBuff((ObjAIBase)target, target, new Buffs.Potion_Internal(), 1, 1, 15, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                charVars.CountHealthPotion = 0;
            }
        }
    }
}