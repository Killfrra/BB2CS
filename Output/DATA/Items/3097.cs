#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3097 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(!owner.IsDead)
                {
                    if(owner is Champion)
                    {
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            if(owner == unit)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.EmblemOfValorParticle(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                            }
                            else
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.EmblemOfValor(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                            }
                        }
                    }
                    else
                    {
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            ObjAIBase caster;
                            caster = GetPetOwner((Pet)owner);
                            if(unit == owner)
                            {
                                if(GetBuffCountFromCaster(owner, caster, nameof(Buffs.EmblemOfValor)) == 0)
                                {
                                    AddBuff((ObjAIBase)owner, unit, new Buffs.EmblemOfValorParticle(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                                }
                            }
                            if(unit == owner)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.EmblemOfValorParticle(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                            }
                            else if(unit != caster)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.EmblemOfValor(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
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
    public class _3097 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        Particle emblemParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.emblemParticle, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_CASTER, default, default, true, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.emblemParticle);
        }
    }
}