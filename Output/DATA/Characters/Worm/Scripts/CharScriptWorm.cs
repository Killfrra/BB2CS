#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptWorm : BBCharScript
    {
        Region bubble; // UNUSED
        public override void OnUpdateStats()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ResistantSkin(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 60, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.WormRecouperateOn(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.WrathTimer)) == 0)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SweepTimer)) == 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PropelTimer)) == 0)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ActionTimer2)) == 0)
                        {
                            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ActionTimer)) == 0)
                            {
                                float distance;
                                distance = DistanceBetweenObjects("Attacker", "Owner");
                                if(distance <= 950)
                                {
                                    FaceDirection(owner, attacker.Position);
                                    SpellCast((ObjAIBase)owner, attacker, owner.Position, owner.Position, 3, SpellSlotType.SpellSlots, 1, false, false, false, false, false, false);
                                }
                                else
                                {
                                    damageAmount = 0;
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            SpellBuffRemove(owner, nameof(Buffs.ActionTimer), (ObjAIBase)owner);
            SpellBuffRemove(owner, nameof(Buffs.PropelTimer), (ObjAIBase)owner);
            SpellBuffRemove(owner, nameof(Buffs.WrathTimer), (ObjAIBase)owner);
            SpellBuffRemove(owner, nameof(Buffs.SweepTimer), (ObjAIBase)owner);
            if(RandomChance() < 0.04f)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.WrathCooldown)) > 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.ActionTimer(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.WrathCooldown)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.WrathTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.WrathCooldown(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
            else if(RandomChance() < 0.12f)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.SweepTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
            else if(RandomChance() < 0.18f)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.PropelTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.ActionTimer(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.WrathTimer)) == 0)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SweepTimer)) == 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PropelTimer)) == 0)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ActionTimer)) == 0)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.ActionTimer2(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = TeamId.TEAM_NEUTRAL;
            this.bubble = AddPosPerceptionBubble(teamID, 1600, owner.Position, 25000, default, false);
            SetImmovable(owner, true);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}