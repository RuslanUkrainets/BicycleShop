using BicycleShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace BicycleShop.Data
{
    public static class DBInitializer
    {
        public static void Initialize(BicycleContext context)
        {
            context.Database.EnsureCreated();

            if (context.Bicycles.Any()) return;

            var bicycles = new List<Bicycle>
            {
                new Bicycle
                {
                    Name = "Cross Focus",
                    WheelDiameter = "26",
                    Color = "Blue",
                    Chain = "KMC, C30 108L",
                    Weight = "14",
                    Year = "2021",
                    Price = 5000
                },
                new Bicycle
                {
                    Name = "Corrado Fortun",
                    WheelDiameter = "26",
                    Color = "Black",
                    Chain = "KMC, C30 108L",
                    Weight = "16",
                    Year = "2019",
                    Price = 9200
                },
                new Bicycle
                {
                    Name = "Ardis City Line",
                    WheelDiameter = "24",
                    Color = "Green",
                    Chain = "KMC, C30 108L",
                    Weight = "25",
                    Year = "2019",
                    Price = 6600
                },
                new Bicycle
                {
                    Name = "Dorozhnik COMFORT FEMALE",
                    WheelDiameter = "28",
                    Color = "Turquoise",
                    Chain = "KMC Z-410",
                    Weight = "18",
                    Year = "2021",
                    Price = 5200
                },
                new Bicycle
                {
                    Name = "Ardis Titan",
                    WheelDiameter = "29",
                    Color = "Gray",
                    Chain = "KMC Z72",
                    Weight = "15",
                    Year = "2021",
                    Price = 5000
                },
                new Bicycle
                {
                    Name = "Royal Baby Chipmunk Explorer",
                    WheelDiameter = "20",
                    Color = "Blue",
                    Chain = "KMC, C30 108L",
                    Weight = "14.5",
                    Year = "2021",
                    Price = 5400
                },
                new Bicycle
                {
                    Name = "Crossride Skyline",
                    WheelDiameter = "24",
                    Color = "Yellow",
                    Chain = "KMC, C30 108L",
                    Weight = "15",
                    Year = "2020",
                    Price = 4300
                },
                new Bicycle
                {
                    Name = "Discovery Flipper",
                    WheelDiameter = "24",
                    Color = "Orange",
                    Chain = "TEC",
                    Weight = "16",
                    Year = "2021",
                    Price = 5200
                },
                new Bicycle
                {
                    Name = "Orbea MX50",
                    WheelDiameter = "27.5",
                    Color = "Red",
                    Chain = "KMC X8",
                    Weight = "15",
                    Year = "2021",
                    Price = 16_500
                },
                new Bicycle
                {
                    Name = "Dorozhnik LUX",
                    WheelDiameter = "26",
                    Color = "Dark-Blue",
                    Chain = "KMC Z-410",
                    Weight = "18",
                    Year = "2021",
                    Price = 5000
                },
                new Bicycle
                {
                    Name = "Discovery RANGER",
                    WheelDiameter = "26",
                    Color = "Black",
                    Chain = "TEC",
                    Weight = "17",
                    Year = "2021",
                    Price = 5000
                },
                new Bicycle
                {
                    Name = "Atlantic Fusion NS",
                    WheelDiameter = "24",
                    Color = "Yellow",
                    Chain = "JX C50",
                    Weight = "15",
                    Year = "2021",
                    Price = 4900
                },
                new Bicycle
                {
                    Name = "Crossride Spider",
                    WheelDiameter = "26",
                    Color = "Black",
                    Chain = "KMC, C30 108L",
                    Weight = "15",
                    Year = "2020",
                    Price = 5100
                },
                new Bicycle
                {
                    Name = "CrossBike Leader",
                    WheelDiameter = "29",
                    Color = "Blue",
                    Chain = "FFC 114L",
                    Weight = "18",
                    Year = "2018",
                    Price = 6000
                },
                new Bicycle
                {
                    Name = "Orbea MX40",
                    WheelDiameter = "29",
                    Color = "Red",
                    Chain = "Shimano HG53 9-Speed",
                    Weight = "15",
                    Year = "2021",
                    Price = 19_600
                },
                new Bicycle
                {
                    Name = "Cross Focus",
                    WheelDiameter = "26",
                    Color = "Blue",
                    Chain = "KMC, C30 108L",
                    Weight = "14",
                    Year = "2021",
                    Price = 5000
                },
                new Bicycle
                {
                    Name = "Ardis Corsair",
                    WheelDiameter = "26",
                    Color = "Blue",
                    Chain = "KMC Z72",
                    Weight = "16",
                    Year = "2021",
                    Price = 11_000
                },
                new Bicycle
                {
                    Name = "Spirit Echo",
                    WheelDiameter = "29",
                    Color = "Gray",
                    Chain = "KMC Z99",
                    Weight = "16",
                    Year = "2020",
                    Price = 20_800
                },
                new Bicycle
                {
                    Name = "Corrado Kanio",
                    WheelDiameter = "26",
                    Color = "White",
                    Chain = "KMC, C30 108L",
                    Weight = "14",
                    Year = "2019",
                    Price = 9700
                },
                new Bicycle
                {
                    Name = "Discovery Galactik",
                    WheelDiameter = "24",
                    Color = "Blue",
                    Chain = "TEC",
                    Weight = "16",
                    Year = "2021",
                    Price = 5500
                },
                new Bicycle
                {
                    Name = "Leon TN-70",
                    WheelDiameter = "29",
                    Color = "Gray",
                    Chain = "KMC Z9",
                    Weight = "15",
                    Year = "2021",
                    Price = 17_000
                },
                new Bicycle
                {
                    Name = "Ardis Hiland",
                    WheelDiameter = "27.5",
                    Color = "Black",
                    Chain = "KMC, C30 108L",
                    Weight = "15",
                    Year = "2020",
                    Price = 7200
                },
                new Bicycle
                {
                    Name = "Cross Focus",
                    WheelDiameter = "26",
                    Color = "Blue",
                    Chain = "KMC, C30 108L",
                    Weight = "14",
                    Year = "2021",
                    Price = 5000
                },
                new Bicycle
                {
                    Name = "Discovery Galactik",
                    WheelDiameter = "24",
                    Color = "Yellow",
                    Chain = "TEC",
                    Weight = "16",
                    Year = "2021",
                    Price = 5500
                },
                new Bicycle
                {
                    Name = "VNC Expance",
                    WheelDiameter = "26",
                    Color = "Black",
                    Chain = "JX C50",
                    Weight = "14",
                    Year = "2021",
                    Price = 8000
                },
                new Bicycle
                {
                    Name = "Stolen Casino",
                    WheelDiameter = "20",
                    Color = "Black",
                    Chain = "Z-410",
                    Weight = "11",
                    Year = "2021",
                    Price = 12_000
                },
                new Bicycle
                {
                    Name = "Discovery Rocket",
                    WheelDiameter = "24",
                    Color = "Red",
                    Chain = "KMC, C30 108L",
                    Weight = "17",
                    Year = "2021",
                    Price = 5500
                },
                new Bicycle
                {
                    Name = "Ardis Silver Bike",
                    WheelDiameter = "26",
                    Color = "Silver",
                    Chain = "KMC",
                    Weight = "16",
                    Year = "2019",
                    Price = 6600
                },
                new Bicycle
                {
                    Name = "Discovery Canyon",
                    WheelDiameter = "26",
                    Color = "Red",
                    Chain = "KMC, C30 108L",
                    Weight = "18",
                    Year = "2021",
                    Price = 5600
                },
                new Bicycle
                {
                    Name = "Ardis Dinamic",
                    WheelDiameter = "26",
                    Color = "Gray",
                    Chain = "КMC",
                    Weight = "15",
                    Year = "2021",
                    Price = 14_000
                },
            };

            context.AddRange(bicycles);
            context.SaveChanges();
        }
    }
}
