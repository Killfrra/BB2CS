#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3100 : BBItemScript
    {
        int cooldownResevoir; // UNUSED
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            float abilityPower;
            float baseDamage;
            float nextBuffVars_BaseDamage;
            float nextBuffVars_AbilityPower;
            if(spellVars.DoesntTriggerSpellCasts)
            {
            }
            else
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
                {
                    abilityPower = GetFlatMagicDamageMod(owner);
                    baseDamage = GetBaseAttackDamage(owner);
                    nextBuffVars_BaseDamage = baseDamage;
                    nextBuffVars_AbilityPower = abilityPower;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.LichBane(nextBuffVars_AbilityPower), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                }
            }
        }
        public override void OnActivate()
        {
            this.cooldownResevoir = 0;
        }
    }
}