﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buddy.Coroutines;
using ff14bot;
using ff14bot.Managers;
using Magitek.Extensions;
using Magitek.Models.Ninja;
using Magitek.Models.QueueSpell;
using Magitek.Utilities;

namespace Magitek.Logic.Ninja
{
    internal static class Ninjutsu
    {
        public static bool FumaShuriken()
        {
            if (!NinjaSettings.Instance.UseFumaShuriken)
                return false;

            if (!ActionManager.HasSpell(Spells.FumaShuriken.Id))
                return false;

            if (Core.Me.ClassLevel < 30)
                return false;

            // First Mudra of the line
            if (!ActionManager.CanCast(Spells.Ten, null))
                return false;

            if (Spells.TrickAttack.Cooldown.Seconds < 22)
                return false;

            // Basic checks
            if (!Core.Me.CurrentTarget.InLineOfSight())
                return false;

            if (Core.Me.CurrentTarget.Distance(Core.Me) > 25)
                return false;

            SpellQueueLogic.SpellQueue.Clear();
            SpellQueueLogic.Timeout.Start();
            SpellQueueLogic.CancelSpellQueue = () => SpellQueueLogic.Timeout.ElapsedMilliseconds > 5000;
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Ten, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.FumaShuriken });
            return true;
        }

        public static bool Huton()
        {
            if (!NinjaSettings.Instance.UseHuton)
                return false;

            if (!ActionManager.HasSpell(Spells.Huton.Id))
                return false;

            if (Core.Me.ClassLevel < 45)
                return false;

            if (!ActionManager.CanCast(Spells.Jin, null))
                return false;

            if (ActionResourceManager.Ninja.HutonTimer.TotalMilliseconds > 20000)
                return false;

            if (ActionManager.HasSpell(Spells.ArmorCrush.Id))
            {
                if (ActionManager.LastSpell == Spells.GustSlash)
                    return false;
            }

            SpellQueueLogic.SpellQueue.Clear();
            SpellQueueLogic.Timeout.Start();
            SpellQueueLogic.CancelSpellQueue = () => SpellQueueLogic.Timeout.ElapsedMilliseconds > 5000;
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Jin, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Chi, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Ten, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Huton, TargetSelf = true });
            return true;
        }

        public static bool GokaMekkyaku()
        {
            if (!NinjaSettings.Instance.UseGokaMekkyaku)
                return false;

            if (!ActionManager.HasSpell(Spells.GokaMekkyaku.Id))
                return false;

            if (Core.Me.ClassLevel < 76)
                return false;

            if (!ActionManager.CanCast(Spells.Chi, null))
                return false;

            if (!Core.Me.HasAura(Auras.Kassatsu) && Casting.LastSpell != Spells.Kassatsu)
                return false;

            if (Core.Me.EnemiesNearby(8).Count() < NinjaSettings.Instance.GokaMekkyakuMinEnemies)
                return false;

            SpellQueueLogic.SpellQueue.Clear();
            SpellQueueLogic.Timeout.Start();
            SpellQueueLogic.CancelSpellQueue = () => SpellQueueLogic.Timeout.ElapsedMilliseconds > 5000;
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Chi, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Ten, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.GokaMekkyaku });
            return true;
        }

        public static bool Doton()
        {
            if (!NinjaSettings.Instance.UseDoton)
                return false;

            if (MovementManager.IsMoving)
                return false;

            if (!ActionManager.HasSpell(Spells.Doton.Id))
                return false;

            if (Core.Me.ClassLevel < 45)
                return false;

            if (!ActionManager.CanCast(Spells.Ten, null))
                return false;

            if (Core.Me.HasAura(Auras.Doton))
                return false;

            if (Core.Me.EnemiesNearby(8).Count() < NinjaSettings.Instance.DotonMinEnemies)
                return false;

            SpellQueueLogic.SpellQueue.Clear();
            SpellQueueLogic.Timeout.Start();
            SpellQueueLogic.CancelSpellQueue = () => SpellQueueLogic.Timeout.ElapsedMilliseconds > 5000;
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Ten, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Jin, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Chi, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Doton, TargetSelf = true });
            return true;
        }

        public static bool Katon()
        {
            if (!NinjaSettings.Instance.UseKaton)
                return false;

            if (!NinjaSettings.Instance.UseAoe)
                return false;

            if (Core.Me.CurrentTarget.Distance(Core.Me) > 15)
                return false;

            if (!ActionManager.HasSpell(Spells.Katon.Id))
                return false;

            if (Core.Me.ClassLevel < 35)
                return false;

            if (!ActionManager.CanCast(Spells.Chi, null))
                return false;

            if (Core.Me.CurrentTarget.EnemiesNearby(5 + Core.Me.CurrentTarget.CombatReach).Count() < NinjaSettings.Instance.KatonMinEnemies)
                return false;

            SpellQueueLogic.SpellQueue.Clear();
            SpellQueueLogic.Timeout.Start();
            SpellQueueLogic.CancelSpellQueue = () => SpellQueueLogic.Timeout.ElapsedMilliseconds > 5000;
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Chi, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Ten, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Katon });
            return true;
        }

        public static bool HyoshoRanryu()
        {
            if (!NinjaSettings.Instance.UseHyoshoRanryu)
                return false;

            if (!ActionManager.HasSpell(Spells.HyoshoRanryu.Id))
                return false;

            if (Core.Me.ClassLevel < 76)
                return false;

            if (!ActionManager.CanCast(Spells.Chi, null))
                return false;

            if (!Core.Me.HasAura(Auras.Kassatsu) && Casting.LastSpell != Spells.Kassatsu)
                return false;

            SpellQueueLogic.SpellQueue.Clear();
            SpellQueueLogic.Timeout.Start();
            SpellQueueLogic.CancelSpellQueue = () => SpellQueueLogic.Timeout.ElapsedMilliseconds > 5000;
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Chi, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Jin, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.HyoshoRanryu });
            return true;
        }

        public static bool Raiton()
        {
            if (!NinjaSettings.Instance.UseRaiton)
                return false;

            if (!ActionManager.HasSpell(Spells.Chi.Id))
                return false;

            if (Core.Me.ClassLevel < 35)
                return false;

            if (!ActionManager.CanCast(Spells.Ten, null))
                return false;

            SpellQueueLogic.SpellQueue.Clear();
            SpellQueueLogic.Timeout.Start();
            SpellQueueLogic.CancelSpellQueue = () => SpellQueueLogic.Timeout.ElapsedMilliseconds > 5000;
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Ten, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Chi, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Raiton });
            return true;
        }

        public static bool Suiton()
        {
            if (Core.Me.HasAura(Auras.Suiton))
                return false;

            if (!NinjaSettings.Instance.UseTrickAttack)
                return false;

            if (!ActionManager.HasSpell(Spells.Suiton.Id))
                return false;

            if (Core.Me.ClassLevel < 45)
                return false;

            if (!ActionManager.CanCast(Spells.Ten, null))
                return false;

            if (Spells.TrickAttack.Cooldown.TotalMilliseconds > 5000)
                return false;

            SpellQueueLogic.SpellQueue.Clear();
            SpellQueueLogic.Timeout.Start();
            SpellQueueLogic.CancelSpellQueue = () => SpellQueueLogic.Timeout.ElapsedMilliseconds > 5000;
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Ten, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Chi, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Jin, TargetSelf = true });
            SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell { Spell = Spells.Suiton });
            return true;
        }

        public static async Task<bool> TenChiJin()
        {
            if (!NinjaSettings.Instance.UseTenChiJin)
                return false;

            if (!NinjaSettings.Instance.UseTrickAttack)
                return false;

            if (MovementManager.IsMoving)
                return false;

            if (Spells.Jin.Cooldown.Seconds < 3)
                return false;

            if (!await Spells.TenChiJin.Cast(Core.Me))
                return false;

            if (!await Coroutine.Wait(2000, () => Core.Me.HasAura(Auras.TenChiJin)))
                return false;

            if (Spells.TrickAttack.Cooldown.Seconds < 8)
            {
                SpellQueueLogic.SpellQueue.Clear();
                SpellQueueLogic.Timeout.Start();
                SpellQueueLogic.CancelSpellQueue = () => SpellQueueLogic.Timeout.ElapsedMilliseconds > 10000;

                SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell
                {
                    Spell = Spells.Ten,
                    TargetSelf = true,
                    Checks = new List<QueueSpellCheck>()
                {
                    new QueueSpellCheck() { Check = () => Core.Me.HasAura(Auras.TenChiJin), Name = "HasTCJ" }
                }

                });

                SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell
                {
                    Spell = Spells.FumaShuriken,
                    Checks = new List<QueueSpellCheck>()
                {
                    new QueueSpellCheck() { Check = () => Core.Me.HasAura(Auras.TenChiJin), Name = "HasTCJ" }
                }
                });

                SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell
                {
                    Spell = Spells.Chi,
                    TargetSelf = true,
                    Checks = new List<QueueSpellCheck>()
                {
                    new QueueSpellCheck() { Check = () => Core.Me.HasAura(Auras.TenChiJin), Name = "HasTCJ" }
                }
                });

                SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell
                {
                    Spell = Spells.Raiton,
                    Checks = new List<QueueSpellCheck>()
                {
                    new QueueSpellCheck() { Check = () => Core.Me.HasAura(Auras.TenChiJin), Name = "HasTCJ" }
                }
                });

                SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell
                {
                    Spell = Spells.Jin,
                    SleepBefore = true,
                    SleepMilliseconds = 500,
                    TargetSelf = true,
                    Wait = new QueueSpellWait() { Check = () => Spells.Jin.Cooldown == TimeSpan.Zero, Name = "Jin", WaitTime = 2000 },
                    Checks = new List<QueueSpellCheck>()
                {
                    new QueueSpellCheck() { Check = () => Core.Me.HasAura(Auras.TenChiJin), Name = "HasTCJ" }
                }
                });

                SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell
                {
                    Spell = Spells.Suiton,
                    SleepBefore = true,
                    SleepMilliseconds = 500,
                    Wait = new QueueSpellWait() { Check = () => Spells.Suiton.Cooldown == TimeSpan.Zero, Name = "Jin", WaitTime = 2000 },
                    Checks = new List<QueueSpellCheck>()
                {
                    new QueueSpellCheck() { Check = () => Core.Me.HasAura(Auras.TenChiJin), Name = "HasTCJ" }
                }

                });
                return true;
            }

            if (Spells.TrickAttack.Cooldown.Seconds > 15)
            {
                SpellQueueLogic.SpellQueue.Clear();
                //SpellQueueLogic.CancelSpellQueue = () => !Core.Me.HasTarget || !Core.Me.HasAura(Auras.TenChiJin);
                SpellQueueLogic.Timeout.Start();
                SpellQueueLogic.CancelSpellQueue = () => SpellQueueLogic.Timeout.ElapsedMilliseconds > 10000;

                SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell
                {
                    Spell = Spells.Jin,
                    TargetSelf = true,
                    Checks = new List<QueueSpellCheck>()
                {
                    new QueueSpellCheck() { Check = () => Core.Me.HasAura(Auras.TenChiJin), Name = "HasTCJ" }
                }

                });

                SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell
                {
                    Spell = Spells.FumaShuriken,
                    Checks = new List<QueueSpellCheck>()
                {
                    new QueueSpellCheck() { Check = () => Core.Me.HasAura(Auras.TenChiJin), Name = "HasTCJ" }
                }
                });

                SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell
                {
                    Spell = Spells.Ten,
                    TargetSelf = true,
                    SleepBefore = true,
                    SleepMilliseconds = 500,
                    Checks = new List<QueueSpellCheck>()
                {
                    new QueueSpellCheck() { Check = () => Core.Me.HasAura(Auras.TenChiJin), Name = "HasTCJ" }
                }
                });

                SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell
                {
                    Spell = Spells.Katon,
                    SleepBefore = true,
                    SleepMilliseconds = 500,
                    Checks = new List<QueueSpellCheck>()
                {
                    new QueueSpellCheck() { Check = () => Core.Me.HasAura(Auras.TenChiJin), Name = "HasTCJ" }
                }
                });

                SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell
                {
                    Spell = Spells.Chi,
                    SleepBefore = true,
                    SleepMilliseconds = 500,
                    TargetSelf = true,
                    Wait = new QueueSpellWait() { Check = () => Spells.Jin.Cooldown == TimeSpan.Zero, Name = "Jin", WaitTime = 2000 },
                    Checks = new List<QueueSpellCheck>()
                {
                    new QueueSpellCheck() { Check = () => Core.Me.HasAura(Auras.TenChiJin), Name = "HasTCJ" }
                }
                });

                SpellQueueLogic.SpellQueue.Enqueue(new QueueSpell
                {
                    Spell = Spells.Doton,
                    TargetSelf = true,
                    SleepBefore = true,
                    SleepMilliseconds = 500,
                    Wait = new QueueSpellWait() { Check = () => Spells.Doton.Cooldown == TimeSpan.Zero, Name = "Jin", WaitTime = 2000 },
                    Checks = new List<QueueSpellCheck>()
                {
                    new QueueSpellCheck() { Check = () => Core.Me.HasAura(Auras.TenChiJin), Name = "HasTCJ" }
                }

                });
                return true;
            }
            return false;
        }
    }
}
