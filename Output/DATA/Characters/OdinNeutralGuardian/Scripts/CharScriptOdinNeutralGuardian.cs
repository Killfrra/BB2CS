#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptOdinNeutralGuardian : BBCharScript
    {
        public override void OnActivate()
        {
            int nextBuffVars_HPPerLevel; // UNUSED
            int nextBuffVars_DmgPerLevel;
            int nextBuffVars_ArmorPerLevel; // UNUSED
            int nextBuffVars_MR_per_level; // UNUSED
            SetImmovable(owner, true);
            SetDodgePiercing(owner, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinGuardianBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinGuardianUI(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.TurretDamageManager(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 1, true, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, false))
            {
                AddBuff((ObjAIBase)unit, unit, new Buffs.OdinPlayerBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            nextBuffVars_HPPerLevel = 75;
            nextBuffVars_DmgPerLevel = 15;
            nextBuffVars_ArmorPerLevel = 4;
            nextBuffVars_MR_per_level = 2;
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinGuardianStatsByLevel(nextBuffVars_DmgPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}