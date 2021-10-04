using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public struct FluidStats
    {
        public int AlcoholicStrength;
        public int CaffeineLevel;
        public int EnergyLevel;
        public Color FluidColour;
        public int MentalStability;
        public int Sourness;
        public int Sweetness;
        
        public static FluidStats Add(FluidStats me, FluidStats other)
        {
            return new FluidStats
            {
                FluidColour = other.FluidColour + me.FluidColour / 2,
                Sweetness = other.Sweetness + me.Sweetness,
                Sourness = other.Sourness + me.Sourness,
                AlcoholicStrength = other.AlcoholicStrength + me.AlcoholicStrength,
                CaffeineLevel = other.CaffeineLevel + me.CaffeineLevel,
                EnergyLevel = other.EnergyLevel + me.EnergyLevel,
                MentalStability = other.MentalStability + me.MentalStability
            };
        }

        public static FluidStats operator +(FluidStats a, FluidStats b) => FluidStats.Add(a, b);
    }

    [Serializable]
    public struct StatMod
    {
        public MathmaticalOperator MathmaticalOperator;
        public int Amount;

        public char MathmaticalSymbol()
        {
            if (Amount == 0)
            {
                return ' ';
            }

            switch (MathmaticalOperator)
            {
                case MathmaticalOperator.Add:
                    return '+';
                case MathmaticalOperator.Subtract:
                    return '-';
                case MathmaticalOperator.Multiply:
                    return '*';
                case MathmaticalOperator.Divide:
                    return '/';
            }

            return ' ';
        }
    }

    public enum MathmaticalOperator
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }
}