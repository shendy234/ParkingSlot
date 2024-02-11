using System;
using System.Collections.Generic;
using System.Linq;

namespace coba{
public enum PlateType
{
    Odd,
    Even
}
class Program
{
    static List<ParkingSlot> parkingSlots;
    static List<Vehicle> parkedVehicles;

    static void Main()
    {
        Console.Write("Masukkan jumlah total Slot: ");
        int totalSlots = int.Parse(Console.ReadLine());
        InitializeParkingSlots(totalSlots);

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Check-In");
            Console.WriteLine("2. Check-Out");
            Console.WriteLine("3. Laporan");
            Console.WriteLine("4. Nomor registrasi kendaraan dengan plat ganjil");
            Console.WriteLine("5. Nomor registrasi kendaraan dengan plat genap");
            Console.WriteLine("6. Nomor registrasi kendaraan dengan warna tertentu");
            Console.WriteLine("7. Nomor slot untuk kendaraan dengan warna tertentu");
            Console.WriteLine("8. Nomor slot untuk nomor registrasi kendaraan");
            Console.WriteLine("9. Jumlah kendaraan berdasarkan jenis");
            Console.WriteLine("10. Keluar");

            Console.Write("Pilih opsi: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CheckIn();
                    break;
                case "2":
                    CheckOut();
                    break;
                case "3":
                    GenerateReport();
                    break;
                case "4":
                    FilterRegistrationNumbersByPlateType(PlateType.Odd);
                    break;
                case "5":
                    FilterRegistrationNumbersByPlateType(PlateType.Even);
                    break;
                case "6":
                    FilterRegistrationNumbersByColor();
                    break;
                case "7":
                    FilterSlotNumbersByColor();
                    break;
                case "8":
                    GetSlotNumberForRegistrationNumber();
                    break;
                case "9":
                    CountVehiclesByType();
                    break;
                case "10":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opsi tidak valid. Silakan coba lagi.");
                    break;
            }
        }
    }

    static void InitializeParkingSlots(int totalSlots)
    {
        parkingSlots = Enumerable.Range(1, totalSlots).Select(slotNumber => new ParkingSlot(slotNumber)).ToList();
        parkedVehicles = new List<Vehicle>();
    }

    static void CheckIn()
    {
        Console.Write("Masukkan nomor polisi kendaraan: ");
        string plateNumber = Console.ReadLine();

        Console.Write("Masukkan jenis kendaraan (Mobil/Motor): ");
        string vehicleType = Console.ReadLine();

        Console.Write("Masukkan warna kendaraan: ");
        string vehicleColor = Console.ReadLine();

        if (vehicleType.ToLower() == "mobil" || vehicleType.ToLower() == "motor")
        {
            Vehicle newVehicle = new Vehicle(plateNumber, vehicleType, vehicleColor);
            ParkingSlot availableSlot = parkingSlots.FirstOrDefault(Slot => !Slot.IsOccupied);

            if (availableSlot != null)
            {
                availableSlot.OccupySlot(newVehicle);
                parkedVehicles.Add(newVehicle);

                Console.WriteLine($"Kendaraan dengan nomor polisi {plateNumber} berhasil check-in di Slot {availableSlot.SlotNumber}.");
            }
            else
            {
                Console.WriteLine("Maaf, semua Slot sudah terisi. Tidak dapat check-in.");
            }
        }
        else
        {
            Console.WriteLine("Jenis kendaraan tidak valid. Hanya Mobil dan Motor yang diperbolehkan.");
        }
    }

    static void CheckOut()
    {
        Console.Write("Masukkan nomor polisi kendaraan: ");
        string plateNumber = Console.ReadLine();

        Vehicle vehicleToCheckout = parkedVehicles.FirstOrDefault(vehicle => vehicle.PlateNumber == plateNumber);

        if (vehicleToCheckout != null)
        {
            ParkingSlot occupiedSlot = parkingSlots.FirstOrDefault(slot => slot.OccupiedBy == vehicleToCheckout);

            if (occupiedSlot != null)
            {
                occupiedSlot.VacateSlot();
                parkedVehicles.Remove(vehicleToCheckout);

                Console.WriteLine($"Kendaraan dengan nomor polisi {plateNumber} berhasil check-out dari Slot {occupiedSlot.SlotNumber}.");
            }
            else
            {
                Console.WriteLine($"Kendaraan dengan nomor polisi {plateNumber} tidak ditemukan di dalam Slot.");
            }
        }
        else
        {
            Console.WriteLine($"Kendaraan dengan nomor polisi {plateNumber} tidak terdaftar.");
        }
    }

    static void GenerateReport()
    {
        Console.WriteLine($"Slot\tNo.\t\tType\t\tColour");

        foreach (var slot in parkingSlots)
        {
            if (slot.IsOccupied)
            {
                Vehicle vehicle = slot.OccupiedBy!;
                Console.WriteLine($"{slot.SlotNumber}\t{vehicle.PlateNumber}\t{vehicle.Type}\t\t{vehicle.Color}");
            }
        }
    }

        static void FilterRegistrationNumbersByPlateType(PlateType plateType)
    {
        var filteredNumbers = parkedVehicles
            .Where(vehicle =>
                (plateType == PlateType.Odd && vehicle?.IsOddPlateNumber == true) ||
                (plateType == PlateType.Even && vehicle?.IsEvenPlateNumber == true))
            .Select(vehicle => vehicle?.PlateNumber)
            .Where(plateNumber => plateNumber != null)
            .ToList();

        Console.WriteLine(string.Join(", ", filteredNumbers));
    }

    static void FilterRegistrationNumbersByColor()
    {
        Console.Write("Masukkan warna kendaraan: ");
        string color = Console.ReadLine();

        var filteredNumbers = parkedVehicles
            .Where(vehicle => vehicle?.Color?.ToLower() == color.ToLower())
            .Select(vehicle => vehicle?.PlateNumber)
            .Where(plateNumber => plateNumber != null)
            .ToList();

        Console.WriteLine(string.Join(", ", filteredNumbers));
    }

        static void FilterSlotNumbersByColor()
    {
        Console.Write("Masukkan warna kendaraan: ");
        string color = Console.ReadLine();

        var filteredSlots = parkingSlots
            .Where(slot => slot.IsOccupied && slot.OccupiedBy?.Color?.ToLower() == color.ToLower())
            .Select(slot => slot.SlotNumber)
            .ToList();

        Console.WriteLine(string.Join(", ", filteredSlots));
    }

    static void GetSlotNumberForRegistrationNumber()
    {
        Console.Write("Masukkan nomor registrasi kendaraan: ");
        string registrationNumber = Console.ReadLine();

        ParkingSlot slot = parkingSlots.FirstOrDefault(l => l.IsOccupied && l.OccupiedBy?.PlateNumber == registrationNumber);

        if (slot != null)
        {
            Console.WriteLine($"Nomor slot untuk nomor registrasi {registrationNumber}: {slot.SlotNumber}");
        }
        else
        {
            Console.WriteLine($"Nomor registrasi {registrationNumber} tidak ditemukan di dalam Slot.");
        }
    }

    static void CountVehiclesByType()
    {
        Console.Write("Masukkan jenis kendaraan (Motor/Mobil): ");
        string type = Console.ReadLine();

        int count = parkedVehicles.Count(vehicle => vehicle?.Type?.ToLower() == type.ToLower());
        Console.WriteLine(count);
    }
    
    }
}
