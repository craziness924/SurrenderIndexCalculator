using System;
using System.Reflection.Emit;

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
            Console.WriteLine($"The punting team has a score of {offensescore}");
            Console.WriteLine();
            Console.WriteLine("Please enter the Defense's score and press enter.");
            defensescore = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"The defensive team has a score of {defensescore}");
            Console.WriteLine();
            Console.WriteLine("Please enter the quarter number, enter 5 for OT.");
            quarter = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Please enter the amount of minutes remaining in the quarter and press enter.");
            minutes = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Please enter the amount of seconds remaining in the quarter.");
            seconds = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"There is {minutes}:{seconds} remaining in Quarter {quarter}");
            Console.WriteLine();
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
            Console.WriteLine("How many yards is the punter from the defense's goal line?");
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
            Console.WriteLine();
            Console.WriteLine();
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
            else if (scoredifferental >= 9) // punter losing by many
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
                bool iscloserthan40butfurtherthan50 = (distance >= 50 && distance < 60);
                double power = Math.Pow(1.1, Math.Min(10, 60 - distance)) * Math.Pow(1.2, Math.Max(0, 60 - distance - 10)); //borrowed code i really need to work tihs out but i dont wanna be stuck on this forever
                double distanceexponent = power;
                distancemulti = Convert.ToDecimal(distanceexponent);
                /*  if (iscloserthan40butfurtherthan50) "borrowed" code works better sorry
                  {
                      double power = Math.Pow(60-distance, 1.1);
                      double distanceexponent = power;
                      distancemulti = Convert.ToDecimal(distanceexponent);
                  }
                  else if (distance < 50)
                  {
                      double power = Math.Pow(1.1, Math.Min(10, 60-distance)) * Math.Pow(1.2, Math.Max(0, 60-distance - 10)); //borrowed code i really need to work tihs out but i dont wanna be stuck on this forever
                      double distanceexponent = power;
                      distancemulti = Convert.ToDecimal(distanceexponent);
                  }    */
            }
            //end field postion calculation
            //start time calculations
            double timemultipler = 1;
            int convertedtotalseconds = 0;
            convertedtotalseconds = (minutes * 60) + seconds;
            int totalquarterseconds = 900;
            if (quarter > 2 && offensescore <= defensescore)
            {
                double secondspasthalf = 0;
                switch (quarter)
                {
                    case 3:
                        secondspasthalf = 0 + (totalquarterseconds - convertedtotalseconds);
                        break;
                    case 4:
                        secondspasthalf = totalquarterseconds + (totalquarterseconds - convertedtotalseconds);
                        break;
                    case 5:
                        secondspasthalf = (2 * totalquarterseconds) + (totalquarterseconds - convertedtotalseconds);
                        break;
                }
                timemultipler = Math.Pow(secondspasthalf * .001, 3) + 1;
            }
            else
            {
                timemultipler = 1;
            }
            //end time calculations 
            //start distancefrom down
            float firstdowndistmulti = 1;
            if (fieldposition == 1)
            {
                firstdowndistmulti = 1f;
            }
            else if (fieldposition == 2 || fieldposition == 3)
            {
                firstdowndistmulti = .8f;
            }
            else if (fieldposition > 3 && fieldposition < 7)
            {
                firstdowndistmulti = .6f;
            }
            else if (fieldposition > 6 && fieldposition < 10)
            {
                firstdowndistmulti = .4f;
            }
            else
            {
                firstdowndistmulti = .2f;
            }
            double surrenderindex = firstdowndistmulti * (float)timemultipler * (float)distancemulti * scorediffmulti;

            Console.WriteLine();
            Console.WriteLine($"Final Index: {Math.Round(surrenderindex, 2)}");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
