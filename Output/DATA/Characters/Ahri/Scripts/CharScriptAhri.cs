#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptAhri : BBCharScript
    {
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher)) > 0)
            {
                spellName = GetSpellName();
                if(spellName == nameof(Spells.AhriOrbofDeception))
                {
                    charVars.OrbofDeceptionIsActive = 1;
                    SpellBuffRemove(owner, nameof(Buffs.AhriPassiveParticle), (ObjAIBase)owner, 0);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AhriIdleCheck(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if(spellName == nameof(Spells.AhriFoxFire))
                {
                    charVars.FoxFireIsActive = 1;
                }
                if(spellName == nameof(Spells.AhriSeduce))
                {
                    charVars.SeduceIsActive = 1;
                    SpellBuffRemove(owner, nameof(Buffs.AhriPassiveParticle), (ObjAIBase)owner, 0);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AhriIdleCheck(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if(spellName == nameof(Spells.AhriTumble))
                {
                    charVars.TumbleIsActive = 1;
                }
            }
            else
            {
                spellName = GetSpellName();
                if(spellName == nameof(Spells.AhriOrbofDeception))
                {
                    SpellBuffRemove(owner, nameof(Buffs.AhriIdleParticle), (ObjAIBase)owner, 0);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AhriIdleCheck(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if(spellName == nameof(Spells.AhriSeduce))
                {
                    SpellBuffRemove(owner, nameof(Buffs.AhriIdleParticle), (ObjAIBase)owner, 0);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AhriIdleCheck(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.AhriIdleParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.OrbofDeceptionIsActive = 0;
            charVars.FoxFireIsActive = 0;
            charVars.SeduceIsActive = 0;
            charVars.TumbleIsActive = 0;
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}