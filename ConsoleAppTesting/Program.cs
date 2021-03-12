using System;

namespace ConsoleAppTesting
{
    class Program
    {

        static void Main(string[] args)
        {
            int offensescore = 0;
            int defensescore = 0;
            int minutes = 0;
            int seconds = 0;
            int quarter = 0;
            int distance = 0;
            string fieldside = "";
            int fieldposition = 0;
            Console.WriteLine("Please enter the Offense's score and press enter.");
            offensescore = Convert.ToInt32(Console.ReadLine());
            Console.Write($"The offensive team has a score of {offensescore}");
            Console.WriteLine();
            Console.WriteLine("Please enter the Defense's score and press enter.");
            defensescore = Convert.ToInt32(Console.ReadLine());
            Console.Write($"The defensive team has a score of {defensescore}");
            Console.WriteLine();
            Console.WriteLine("Please enter the quarter number, enter 5 for OT.");
            quarter = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Please enter amount of minutes remaining in the quarter.");
            minutes = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Please enter amount of seconds remaining in the quarter.");
            seconds = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"There is {minutes}:{seconds} remaining in Quarter {quarter}");
            if (seconds > 60)
            {
                Console.WriteLine("Please enter an amount of secoonds below 60. (The program will not warn you again!)");
                seconds = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"There is {minutes}:{seconds} remaining in Quarter {quarter}");
            }
            if (minutes > 15)
            {
                Console.WriteLine("Please enter an amount of minutes equal to or less than 15. (The program will not warn you again!)");
                minutes = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"There is {minutes}:{seconds} remaining in Quarter {quarter}");
            }
            Console.WriteLine("How many yards is the offense from the defense's goal line?");
            distance = Convert.ToInt32(Console.ReadLine());
            int distanceoutput = 0;
            int distancefromcenter = 0;
            if (distance > 50)
            {
                fieldside = "Own";
                distancefromcenter = -(50 - distance);
                distanceoutput = 50 - distancefromcenter;
            }
            else
            {
                fieldside = "Opponent";
                distanceoutput = distance;
            }

            Console.WriteLine($"The punting team is on their {fieldside} {distanceoutput}");
            Console.WriteLine();
            Console.WriteLine("How many yards was the punting team away from a first down?");
            Console.WriteLine("4th and.....?");
            fieldposition = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Calculating with the following parameters:\nScore: {offensescore}-{defensescore}\nTime: {minutes}:{seconds} remaining in quarter {quarter}\nPunting Spot: {fieldside} {distanceoutput} on 4th and {fieldposition} ");
            Console.ReadKey();
            //calculation start woo!!!!
            //score diff first
            float scoredifferental = Math.Abs(offensescore - defensescore);
            float scorediffmulti = 0f;
            if (offensescore > defensescore) //punter winning
            {
                scorediffmulti = 1f;
            }
            else if (offensescore == defensescore) // punter tied
            {
                scorediffmulti = 2f;
            }
            else if (scoredifferental >= 14) // punter losing by many
            {
                scorediffmulti = 3f;
            }
            else if (scoredifferental < 9) // punter losing by 8 and under
            {
                scorediffmulti = 4f;
            }
            else
            {
                Console.WriteLine("Somehow all the else ifs failed for calculating scorediff multiplier.");
            }
            //end score diff multi
            //start field position calculation
            decimal distancemulti = 0;
            if (distance >= 60)
            {
                distancemulti = 1;
            }
            else
            {
                bool isoutsideof40 = (distance > 50 && distance < 60);
                if (isoutsideof40)
                {
                    double power = Math.Pow(distance, 1.2);
                    double distanceexponent = power;
                    distancemulti = Convert.ToDecimal(distanceexponent);
                }
                else if (distance < 50)
                {
                    double power = Math.Pow(distance, 1.5);
                    double distanceexponent = power;
                    distancemulti = Convert.ToDecimal(distanceexponent);
                }    
                else distancemulti = 0;
            }
            Console.WriteLine(scorediffmulti);
            Console.WriteLine(distancemulti);
            Console.ReadKey();


        
        }
    }
}

