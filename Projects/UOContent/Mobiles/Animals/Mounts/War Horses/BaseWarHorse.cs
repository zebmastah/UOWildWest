using ModernUO.Serialization;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public abstract partial class BaseWarHorse : BaseMount
    {
        public override string DefaultName => "a war horse";

        public BaseWarHorse(
            int bodyID,
            int itemID,
            AIType aiType = AIType.AI_Melee,
            FightMode fightMode = FightMode.Aggressor,
            int rangePerception = DefaultRangePerception,
            int rangeFight = 1
        ) : base(
            bodyID,
            itemID,
            aiType,
            fightMode,
            rangePerception,
            rangeFight
        )
        {
            BaseSoundID = 0xA8;

            InitStats(Utility.Random(300, 100), 125, 60);

            SetStr(400);
            SetDex(125);
            SetInt(51, 55);

            SetHits(240);
            SetMana(0);

            SetDamage(5, 8);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 40, 50);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 25.1, 30.0);
            SetSkill(SkillName.Tactics, 29.3, 44.0);
            SetSkill(SkillName.Wrestling, 29.3, 44.0);

            Fame = 300;
            Karma = 300;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 29.1;
        }

        public override int StepsMax => 6400;
        public override string CorpseName => "a war horse corpse";

        public override FoodType FavoriteFood => FoodType.FruitsAndVeggies | FoodType.GrainsAndHay;
    }
}
