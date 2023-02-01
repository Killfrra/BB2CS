#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Rebirth : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Rebirth",
            BuffTextureName = "Cryophoenix_Rebirth.dds",
            NonDispellable = true,
        };
        bool oneFrame;
        float rebirthArmorMod;
        int seaHorseID; // UNUSED
        Particle eggTimer;
        public Rebirth(float rebirthArmorMod = default)
        {
            this.rebirthArmorMod = rebirthArmorMod;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            float currentCooldown;
            float currentCooldown2;
            teamID = GetTeamID(owner);
            this.oneFrame = true;
            //RequireVar(this.rebirthArmorMod);
            currentCooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            currentCooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(currentCooldown <= 6)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 6);
            }
            if(currentCooldown2 <= 6)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 6);
            }
            this.seaHorseID = PushCharacterData("AniviaEgg", owner, false);
            IncHealth(owner, 10000, owner);
            SpellEffectCreate(out this.eggTimer, out _, "EggTimer.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            SpellBuffRemoveType(owner, BuffType.POISON);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.COMBAT_DEHANCER);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.INVISIBILITY);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.DAMAGE);
            SpellBuffRemoveType(owner, BuffType.HEAL);
            SpellBuffRemoveType(owner, BuffType.HASTE);
            SpellBuffRemoveType(owner, BuffType.SPELL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.PHYSICAL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.INVULNERABILITY);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.POISON);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Particle particle; // UNUSED
            teamID = GetTeamID(owner);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RebirthCooldown(), 1, 1, 240, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            if(!owner.IsDead)
            {
                SpellEffectCreate(out particle, out _, "Rebirth_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
            }
            SpellEffectRemove(this.eggTimer);
            PopAllCharacterData(owner);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            IncFlatArmorMod(owner, this.rebirthArmorMod);
            IncFlatSpellBlockMod(owner, this.rebirthArmorMod);
            this.oneFrame = false;
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(this.oneFrame)
            {
                damageAmount = 0;
            }
        }
    }
}