#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3057 : BBItemScript
    {
        int cooldownResevoir; // UNUSED
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            float baseDamage;
            float nextBuffVars_BaseDamage;
            bool nextBuffVars_IsSheen;
            if(spellVars.DoesntTriggerSpellCasts)
            {
            }
            else
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
                {
                    baseDamage = GetBaseAttackDamage(owner);
                    nextBuffVars_BaseDamage = baseDamage;
                    nextBuffVars_IsSheen = true;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.Sheen(nextBuffVars_BaseDamage, nextBuffVars_IsSheen), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                }
            }
        }
        public override void OnActivate()
        {
            this.cooldownResevoir = 0;
        }
    }
}