using Dice;
using System;
using EnemyFile;
using static NameGenaratorFile.RandomName;

namespace PreMadeEnemies {
    public class Enemies {

        private const Genre MALE = Genre.Male;
        private const Genre FEMALE = Genre.Female;
        private static string Name<T>() {
            return GenerateRandomName<T>(MALE);
        }

        public static Func<BrownBear> BROWNBEAR = () => {
            int health = 20 + Die.Roll(1, Die.d4);
            int armor = 11;
            int initiativeBonus = 1; int xp = 200;
            return new BrownBear(Name<BrownBear>(), health, armor, initiativeBonus, xp);
        };

        // public static Func<ItalianWolf> ITALIANWOLF = () => {
        //     int health = 20 + Die.Roll(1, Die.d4);
        //     int armor = 14;
        //     int initiativeBonus = 2; int xp = 200;
        //     return new ItalianWolf(Name<ItalianWolf>(), health, armor, initiativeBonus, xp);
        // };
        //
        // public static Func<Boar> BOAR = () => {
        //     int health = 12 + Die.Roll(1, Die.d4);
        //     int armor = 11;
        //     int initiativeBonus = 0; int xp = 50;
        //     return new Boar(Name<Boar>(), health, armor, initiativeBonus, xp);
        // };
        //
        // public static Func<BigBoar> BIGBOAR = () => {
        //     int health = 12 + Die.Roll(1, Die.d4);
        //     int armor = 11;
        //     int initiativeBonus = 0; int xp = 50;
        //     return new BigBoar(Name<BigBoar>(), health, armor, initiativeBonus, xp);
        // };
        //
        // public static Func<Deer> DEER = () => {
        //     int health = 9 + Die.Roll(1, Die.d4);
        //     int armor = 10;
        //     int initiativeBonus = 0; int xp = 50;
        //     return new Deer(Name<Deer>(), health, armor, initiativeBonus, xp);
        // };
        //
        // public static Func<BigDeer> BIGDEER = () => {
        //     int health = 9 + Die.Roll(1, Die.d4);
        //     int armor = 10;
        //     int initiativeBonus = 0; int xp = 50;
        //     return new BigDeer(Name<BigDeer>(), health, armor, initiativeBonus, xp);
        // };
        //
        // public static Func<BigRat> BIGRAT = () => {
        //     int health = 7 + Die.Roll(1, Die.d4);
        //     int armor = 13;
        //     int initiativeBonus = 3; int xp = 25;
        //     return new BigRat(Name<BigRat>(), health, armor, initiativeBonus, xp);
        // };
        //

        public static Func<Bandit> banditOne = () => {
            int health = 4 + Die.Roll(1, Die.d2);
            int armor = 10;
            int initiativeBonus = 1; int xp = 25;
            return new Bandit(Name<Bandit>(), health, armor, initiativeBonus, xp);
        };

        public static Func<Bandit> banditTwo = () => {
            int health = 4 + Die.Roll(1, Die.d2);
            int armor = 10;
            int initiativeBonus = 1; int xp = 25;
            return new Bandit(Name<Bandit>(), health, armor, initiativeBonus, xp);
        };

        // public static Func<Hippopotamus> HIPPOPOTAMUS = () => {
        //     int health = 4 + Die.Roll(1, Die.d2);
        //     int armor = 10;
        //     int initiativeBonus = 1; int xp = 10;
        //     return new Hippopotamus(Name<Hippopotamus>(), health, armor, initiativeBonus, xp);
        // };
        //
        // public static Func<Unicorn> UNICORN = () => {
        //     int health = 4 + Die.Roll(1, Die.d2);
        //     int armor = 10;
        //     int initiativeBonus = 1; int xp = 10;
        //     return new Unicorn(Name<Unicorn>(), health, armor, initiativeBonus, xp);
        // };
        //
        // public static Func<TribalWarrior> tribalWarrior = () => {
        //     int health = 4 + Die.Roll(1, Die.d2);
        //     int armor = 10;
        //     int initiativeBonus = 0; int xp = 25;
        //     return new TribalWarrior(Name<TribalWarrior>(), health, armor, initiativeBonus, xp);
        // };
        //
        // public static Func<Plebeian> PLEBIAN = () => {
        //     int health = 4 + Die.Roll(1, Die.d2);
        //     int armor = 10;
        //     int initiativeBonus = 0; int xp = 10;
        //     return new Plebeian(Name<Plebeian>(), health, armor, initiativeBonus, xp);
        // };
        //
        // public static Func<BanditCaptain> banditCaptain = () => {
        //     int health = 4 + Die.Roll(1, Die.d2);
        //     int armor = 10;
        //     int initiativeBonus = 1; int xp = 10;
        //     return new BanditCaptain(Name<BanditCaptain>(), health, armor, initiativeBonus, xp);
        // };
    }
}
