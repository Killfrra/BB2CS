#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FrostShot : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.FrostShot)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.FrostShot), (ObjAIBase)owner, 0);
            }
            else
            {
                float nextBuffVars_ManaCostPerAttack;
                nextBuffVars_ManaCostPerAttack = 8;
                AddBuff(attacker, target, new Buffs.FrostShot(nextBuffVars_ManaCostPerAttack), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class FrostShot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Frost Shot",
            BuffTextureName = "Bowmaster_IceArrow.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 1,
        };
        float manaCostPerAttack;
        public FrostShot(float manaCostPerAttack = default)
        {
            this.manaCostPerAttack = manaCostPerAttack;
        }
        public override void OnActivate()
        {
            //RequireVar(this.manaCostPerAttack);
            OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, false);
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            float temp;
            temp = GetPAR(owner, PrimaryAbilityResourceType.MANA);
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    if(temp >= this.manaCostPerAttack)
                    {
                        float manaToInc;
                        manaToInc = this.manaCostPerAttack * -1;
                        IncPAR(owner, manaToInc, PrimaryAbilityResourceType.MANA);
                    }
                    else
                    {
                        SpellBuffRemoveCurrent(owner);
                    }
                }
            }
        }
    }
}