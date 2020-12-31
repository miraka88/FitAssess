using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;



namespace Fitness
{
    
    

    
    public class Calculations
    {
        public double BMI;
        public double waistHeightRatio;
    }

    public class Assessment
    {

        public string name;

        public string clientName;
        public double weight;
        public double height;
        public double waist;
        
        
    
        public Assessment()
        {
            
        }
        public void GetName(string clientName)
        {
            this.clientName = clientName;
        }
    
        public void GetWeight(double weight)
        {
            this.weight = weight;                   
        }
        public void GetHeight(double height)
        {
            this.height = height;
        }
       
        public void GetWaist(double waist)
        {
            this.waist = waist;
        }

        

        public Calculations Calculate()
        {
            var calculations = new Calculations();

            calculations.BMI = weight / height / height;
            calculations.waistHeightRatio = waist / (height * 100);

            return calculations;
        }

        public void SaveToTextFile(ref string[]results)
        {
            string folder = ""; //file path to store results file
            string filename = $"{clientName}.txt";
            string fullpath = folder + filename;

            File.WriteAllLines(fullpath, results);
            Console.WriteLine($"\nnew file has been created for {clientName}\n");

        }

        public void UpdateExisting(ref string[] results)
        {
            string folder = @"c:\users\ashle\fitness\client info\";
            string filename = $"{clientName}.txt";
            string fullpath = folder + filename;

            using (StreamWriter sw = File.AppendText(fullpath))
            {
                foreach (var result in results)
                {
                    sw.WriteLine(result);
                }

            }
            Console.WriteLine($"Results have been added to {clientName}'s file.\n");
        }

        

    }
    class Program
    {
        static void Main(string[] args)
        {


            var assessment = new Assessment();
            string date = DateTime.UtcNow.ToString("dd-MM-yyyy");

            while (true)
            {


                Console.WriteLine("Welcome to the BASH Strength Assessment App\nPlease Enter Client name, or type quit to exit.");
                var clientName = Console.ReadLine();

                if (clientName == "quit")
                {
                    break;
                }
                assessment.GetName(clientName);

                Console.WriteLine("Thanks, Now Please Enter {0}'s Height in Metres: ", clientName);
                var inputHeight = Console.ReadLine(); 
                var height = Double.Parse(inputHeight);
                assessment.GetHeight(height);
                
                

                Console.WriteLine("Now Please Enter {0}'s Weight in KG: ", clientName);
                var inputWeight = Console.ReadLine();
                var weight = Double.Parse(inputWeight);
                assessment.GetWeight(weight);

                Console.WriteLine("Now Please Enter {0}'s Waist measurement in Centiemetres: ", clientName);
                var inputWaist = Console.ReadLine();
                var waist = Double.Parse(inputWaist);
                assessment.GetWaist(waist);


                var stats = assessment.Calculate();

                string[] results = {$"Date: {date}", $"Name: {assessment.clientName}", $"Weight: {assessment.weight}", $"Height: {assessment.height}",
                        $"Waist: {assessment.waist}\n", $"BMI: {stats.BMI:N0}", $"Height to Waist ratio: {stats.waistHeightRatio:N3}\n"};

                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }

                Console.WriteLine("Would you like to save this Assessment info to a file? (yes/no)?");
                var savetofile = Console.ReadLine();


                if (savetofile == "yes")
                {

                    string folder = @"c:\users\ashle\fitness\client info\";
                    string filename = $"{clientName}.txt";
                    string fullpath = folder + filename;

                    if (!File.Exists(fullpath))
                    {
                        assessment.SaveToTextFile(ref results);
                    }
                    else
                    {
                        assessment.UpdateExisting(ref results);
                    }
                }
                else if(savetofile == "No")
                {
                    Console.WriteLine($"Assessment info for {clientName} not saved.");
                }
                
                


                    
                



            }

        }
    }           
    
}

