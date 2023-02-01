#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaokaiSapMagicChaos : BBBuffScript
    {
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(!spellVars.DoesntTriggerSpellCasts)
            {
                attacker = SetBuffCasterUnit();
                if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MaokaiSapMagicMelee)) == 0)
                {
                    AddBuff(attacker, attacker, new Buffs.MaokaiSapMagicHot(), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
                }
            }
        }
    }
}