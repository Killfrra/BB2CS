#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Teleport_DeathRemoval : BBBuffScript
    {
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            string name;
            ObjAIBase caster;
            name = GetSpellName();
            if(name == nameof(Spells.TeleportCancel))
            {
                caster = SetBuffCasterUnit();
                if(caster is BaseTurret)
                {
                    SpellBuffRemove(caster, nameof(Buffs.Teleport_Turret), (ObjAIBase)owner);
                }
                else
                {
                    SpellBuffRemove(caster, nameof(Buffs.Teleport_Target), (ObjAIBase)owner);
                }
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnDeath()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            if(caster is ObjAIBase)
            {
                if(caster is BaseTurret)
                {
                    SpellBuffRemove(caster, nameof(Buffs.Teleport_Turret), (ObjAIBase)owner);
                }
                else
                {
                    SpellBuffRemove(caster, nameof(Buffs.Teleport_Target), (ObjAIBase)owner);
                }
            }
            SpellBuffRemoveCurrent(owner);
        }
    }
}