using System;
using System.Collections.Generic;
using System.Linq;

namespace coba
{
     public class ParkingSlot
    {
        public int SlotNumber { get; }
        public bool IsOccupied { get; private set; }
        public Vehicle? OccupiedBy { get; private set; } = null;

        public ParkingSlot(int slotNumber)
        {
            SlotNumber = slotNumber;
            IsOccupied = false;
        }

        public void OccupySlot(Vehicle vehicle)
        {
            IsOccupied = true;
            OccupiedBy = vehicle;
        }

        public void VacateSlot()
        {
            IsOccupied = false;
            OccupiedBy = null;
        }
    }

}