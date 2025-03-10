using Godot;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace SpecFreqCustomZone
{
    public static class R
    {
        private static int _printNum = 0;
        private static Random _random;

        public static void Init()
        {
            _random = new Random();
        }

        public static void P(string aFrom, string aText = "")
        {
            string formattedTime = DateTime.Now.ToString("mm:ss"); //HH:mm:ss

            string whatToPrint = _printNum + ": " + formattedTime + " " + aFrom;
            if (aText.Length > 0)
                whatToPrint += " " + aText;

            GD.Print(whatToPrint);
            _printNum++;
        }
        public static float GetRandomFloat(float aMin, float aMax)
        {
            return (float)(aMin + _random.NextDouble() * (aMax - aMin));
        }
        public static int GetRandomNumber(int aMin, int aMax)
        {
            return _random.Next(aMin, aMax);
        }


        public static Vector3 Lerp(Vector3 firstVector, Vector3 secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            float retZ = Lerp(firstVector.Z, secondVector.Z, by);
            return new Vector3(retX, retY, retZ);
        }
        public static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }
    }
}