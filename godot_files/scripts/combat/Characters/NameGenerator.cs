using Godot;
using System;
using System.IO;

using static System.Diagnostics.Debug;

namespace NameGenaratorFile {
   public static class RandomName {
        public enum Genre {
            Male,
            Female
        }

        public static string GenerateRandomName<T>(Genre genre) {
            string maleNamesPath = ProjectSettings.GlobalizePath("res://Texts/MaleNames.txt");
            string femaleNamesPath = ProjectSettings.GlobalizePath("res://Texts/FemaleNames.txt");
            string Craft = typeof(T).Name;

            string[] NameArray;
            string Name;

            switch (genre) {
                case Genre.Male: {
                    NameArray = File.ReadAllLines(maleNamesPath);
                    int randomIndex = Random.Shared.Next(0, NameArray.Length+1);
                    Name = NameArray[randomIndex];
                } break;

                case Genre.Female: {
                    NameArray = File.ReadAllLines(femaleNamesPath);
                    int randomIndex = Random.Shared.Next(0, NameArray.Length+1);
                    Name = NameArray[randomIndex];
                } break;

                default: {
                    Assert(false);
                    Name = "";
                } break;
            }

            return $"{Name}, The {Craft}";
        }
   } 
}
