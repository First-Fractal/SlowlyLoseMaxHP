using System;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace SlowlyLoseMaxHP
{

    internal class SLMHConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static SLMHConfig Instance;

        [Header("LifeOptions")]

        [DefaultValue(1)]
        [Range(1, 15)]
        [Increment(1)]
        [DrawTicks()]
        [Slider()]
        public int lifeCrystalRemove;

        [DefaultValue(4)]
        [Range(1, 20)]
        [Increment(1)]
        [DrawTicks()]
        [Slider()]
        public int lifeFruitRemove;


        [Header("CooldownOptions")]
        [DefaultValue(5)]
        public int generalCooldown;

        [DefaultValue(true)]
        public bool generalCooldownMinute;

        [DefaultValue(true)]
        public bool bossChangeCooldown;

        [DefaultValue(20)]
        public int bossCooldown;

        [DefaultValue(false)]
        public bool bossCooldownMinute;

        [Header("NurseOptions")]
        [DefaultValue(true)]
        public bool nurseSellLifeItems;

        [DefaultValue(1f)]
        [Range(0.1f, 2f)]
        [Increment(0.1f)]
        [DrawTicks()]
        [Slider()]
        public float lifeCrystalMulti;

        [DefaultValue(1f)]
        [Range(0.1f, 2f)]
        [Increment(0.1f)]
        [DrawTicks()]
        [Slider()]
        public float lifeFruitMulti;

        //[DefaultValue(0)]
        //[Range(0, 99)]
        //public int LifeCrystalCopper;

        //[DefaultValue(0)]
        //[Range(0, 99)]
        //public int LifeCrystalSliver;

        //[DefaultValue(10)]
        //[Range(0, 99)]
        //public int LifeCrystalGold;

        //[DefaultValue(0)]
        //[Range(0, 99)]
        //public int LifeCrystalPlat;


        //[DefaultValue(0)]
        //[Range(0, 99)]
        //public int LifeFruitCopper;

        //[DefaultValue(50)]
        //[Range(0, 99)]
        //public int LifeFruitSliver;

        //[DefaultValue(2)]
        //[Range(0, 99)]
        //public int LifeFruitGold;

        //[DefaultValue(0)]
        //[Range(0, 99)]
        //public int LifeFruitPlat;
    }
}
