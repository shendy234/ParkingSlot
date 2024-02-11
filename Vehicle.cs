using System;
using System.Collections.Generic;
using System.Linq;

namespace coba
{
        public class Vehicle
    {
        public string PlateNumber { get; }
        public string Type { get; }
        public string Color { get; }

        public bool IsOddPlateNumber
        {
            get
            {
                if (int.TryParse(PlateNumber[^1].ToString(), out int lastDigit))
                {
                    return lastDigit % 2 != 0;
                }
                return false;
            }
        }
        public bool IsEvenPlateNumber => !IsOddPlateNumber;

        public Vehicle(string plateNumber, string type, string color)
        {
            PlateNumber = plateNumber ?? throw new ArgumentNullException(nameof(plateNumber));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Color = color ?? throw new ArgumentNullException(nameof(color));
        }
    }
}