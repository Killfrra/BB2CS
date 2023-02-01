#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3099 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            float nextBuffVars_ManaRegenMod;
            float nextBuffVars_CooldownReduction;
            ObjAIBase caster;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(!owner.IsDead)
                {
                    nextBuffVars_ManaRegenMod = 2.4f;
                    nextBuffVars_CooldownReduction = -0.1f;
                    if(owner is Champion)
                    {
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            if(unit == owner)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.SoulShroudAuraSelf(nextBuffVars_ManaRegenMod, nextBuffVars_CooldownReduction), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                            else
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.SoulShroudAuraFriend(nextBuffVars_ManaRegenMod, nextBuffVars_CooldownReduction), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                        }
                    }
                    else
                    {
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            caster = GetPetOwner((Pet)owner);
                            if(unit == owner)
                            {
                                if(GetBuffCountFromCaster(owner, caster, nameof(Buffs.SoulShroudAuraFriend)) == 0)
                                {
                                    AddBuff((ObjAIBase)owner, unit, new Buffs.SoulShroudAuraSelf(nextBuffVars_ManaRegenMod, nextBuffVars_CooldownReduction), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                                }
                            }
                            if(unit == owner)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.SoulShroudAuraSelf(nextBuffVars_ManaRegenMod, nextBuffVars_CooldownReduction), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                            else if(unit != caster)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.SoulShroudAuraFriend(nextBuffVars_ManaRegenMod, nextBuffVars_CooldownReduction), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class _3099 : BBBuffScript
    {
        Particle soulShroudParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.soulShroudParticle, out _, "ZettasManaManipulator_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_CASTER, default, default, true, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.soulShroudParticle);
        }
    }
}