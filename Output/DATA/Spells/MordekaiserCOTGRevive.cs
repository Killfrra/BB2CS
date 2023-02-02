#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserCOTGRevive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        Particle mordekaiserParticle;
        public MordekaiserCOTGRevive(Particle mordekaiserParticle = default)
        {
            this.mordekaiserParticle = mordekaiserParticle;
        }
        public override void OnActivate()
        {
            //RequireVar(this.mordekaiserParticle);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.mordekaiserParticle);
        }
        public override void OnUpdateActions()
        {
            if(attacker.IsDead)
            {
                bool zombie;
                zombie = GetIsZombie(attacker);
                if(!zombie)
                {
                    int level; // UNUSED
                    Vector3 pos;
                    Pet other1;
                    float temp1;
                    level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    pos = GetUnitPosition(attacker);
                    other1 = CloneUnitPet(attacker, nameof(Buffs.MordekaiserCOTGPetSlow), 0, pos, 0, 0, false);
                    temp1 = GetMaxHealth(other1, PrimaryAbilityResourceType.MANA);
                    IncHealth(other1, temp1, other1);
                    AddBuff((ObjAIBase)owner, other1, new Buffs.MordekaiserCOTGPetBuff2(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.MordekaiserCOTGRevive));
                }
            }
        }
    }
}