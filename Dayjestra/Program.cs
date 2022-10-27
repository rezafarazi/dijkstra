using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Dayjestra
{
    class Program
    {

        //Variable For D Table
        public static string[,] Value_D;

        //Variable For W Table
        public static string[,] Value_W;

        //Variable For All Calculate Table
        public static int[,] Calculate;

        //Main Function Start
        static void Main(string[] args)
        {
            //Get Graph Function
            GRAPH1();
            Console.ReadKey();
        }
        //Main Function End

        //Graph One Side Function Start        
        public static void GRAPH1()
        {

            //Get All Node Count From User Start
            Console.Write("Enter Number Of Nodes : ");
            int Nodes = int.Parse(Console.ReadLine());
            //Get All Node Count From User End


            //Get All Node Name From User Start
            string[] Nodes_Name = new string[Nodes];

            for (int a = 0; a < Nodes; a++)
            {
                Console.Write("Enter Node {0} Name : ", (a + 1));
                Nodes_Name[a] = Console.ReadLine();
            }
            //Get All Node Name From User End


            // Get All Nodes Value From User And Fill Table W Start
            Value_D = new string[Nodes, Nodes];
            Value_W = new string[Nodes, Nodes];

            for (int a = 0; a < Nodes_Name.Length; a++)
            {
                for (int b = 0; b < Nodes_Name.Length; b++)
                {
                    if (Nodes_Name[a] == Nodes_Name[b])
                    {
                        Value_D[a, b] = 0 + "";
                        Value_W[a, b] = 0 + "";
                    }
                    else
                    {
                        Console.Write("Enter Value Of {0} To {1} (If Not Way Press Enter) : ", Nodes_Name[a], Nodes_Name[b]);
                        string Value = Console.ReadLine();

                        //IF Value From a Node is Zero.Write N On W Table
                        if (Value == null || Value.Trim() == "")
                        {
                            Value_W[a, b] = "N";
                        }
                        else
                        {
                            Value_W[a, b] = int.Parse(Value).ToString();
                        }
                    }
                }
            }
            // Get All Nodes Value From User And Fill Table W End




            //Fill Table D Values From Table W Start
            for (int a = 0; a < Nodes_Name.Length; a++)
            {
                for (int b = 0; b < Nodes_Name.Length; b++)
                {
                    //Get Function Fill Table D By Table W Of a Value
                    W_TO_D(a, b);
                }
            }
            //Fill Table D Values From Table W End


            //Print Table W All Values Start
            Console.WriteLine();

            Console.WriteLine("W:");
            for (int a = 0; a < Nodes_Name.Length; a++)
            {
                for (int b = 0; b < Nodes_Name.Length; b++)
                {
                    Console.Write(Value_W[a, b] + "\t");
                }
                Console.WriteLine("\n");
            }
            //Print Table W All Values End


            //Print Table D All Values Start
            Console.WriteLine();

            Console.WriteLine("D:");
            for (int a = 0; a < Nodes_Name.Length; a++)
            {
                for (int b = 0; b < Nodes_Name.Length; b++)
                {
                    Console.Write(Value_D[a, b] + "\t");
                }
                Console.WriteLine("\n");
            }
            //Print Table D All Values End

        }
        //Graph One Side Function End

        //Function Get Fill Table D Start    
        public static void W_TO_D(int a, int b)
        {

            //Counter For Calculate On Nodes Start
            int DEEP = int.Parse((Value_D.Length / Math.Sqrt(Value_D.Length)).ToString());
            Calculate = new int[DEEP, DEEP];

            for (int i = 0; i < DEEP; i++)
            {
                for (int j = 0; j < DEEP; j++)
                {
                    Calculate[i, j] = 0;
                }
            }
            //Counter For Calculate On Nodes End


            //Set a Value For Fill Table D
            Value_D[a, b] = W(a, b);

        }
        //Function Get Fill Table D End

        //Function Fill Elements D From W Table Elements Start
        public static string W(int a, int b)
        {
            //For Daynamic Programming Start
            if (a == b)
            {
                return "0";
            }
            if (Value_W[a, b] == "1")
            {
                return "1";
            }
            Calculate[a, b]++;
            //For Daynamic Programming End


            //Get a Row Count From   Value_D    Array
            int DEEP = int.Parse((Value_D.Length / Math.Sqrt(Value_D.Length)).ToString());

            //Create a Array For Check All Path And Select Minimum Value
            int[] Paths = new int[DEEP];


            //Loop For Check All Path And Calculate Value From Value_W Table Start
            for (int i = 0; i < DEEP; i++)
            {
                try
                {
                    if (int.Parse(Value_W[i, b]) > 1)
                    {
                        Paths[i] = int.Parse(Value_W[i, b]);
                    }
                }
                catch
                {
                    Paths[i] = int.MaxValue;
                }
            }
            //Loop For Check All Path And Calculate Value From Value_W Table End


            //Loop For Get Minumum Value From Path Array And Get Minimum Index Value Start
            int[] Min_index = new int[DEEP];
            int Min = int.MaxValue;

            for (int i = 0; i < DEEP; i++)
            {
                if (Paths[i] != 0)
                {
                    if (Min > Paths[i])
                    {
                        Min = Paths[i];
                        Min_index[i] = i;
                    }
                }
            }
            //Loop For Get Minumum Value From Path Array And Get Minimum Index Value End



            //Sort All Min Index Value Of Low To UP Start
            Array.Sort(Min_index);
            Array.Reverse(Min_index);
            //Sort All Min Index Value Of Low To UP End



            //Get Mins Array Start//
            int[] Ary = new int[2];
            //Get Mins Array End//



            //If Value_W Table Equals "N" Run Start
            if (Value_W[a, b] != "N")
            {

                //Check Calculate Table And Select Minimum Value From Value_W Array Start
                for (int i = 0; i < DEEP; i++)
                {
                    //If Calculate From a Path Is Lowwer Table Colnum Count Start
                    if (Calculate[a, b] < DEEP)
                    {
                        try
                        {
                            Ary[0] = int.Parse(Math.Min((int.Parse(Value_W[a, Min_index[i]]) + Min), int.Parse(Value_W[a, b])).ToString());
                        }
                        catch
                        {
                            Ary[0] = Min + int.Parse(W(a, Min_index[i])) + Min;
                        }
                        break;
                    }
                    //If Calculate From a Path Is Lowwer Table Colnum Count End

                }
                //Check Calculate Table And Select Minimum Value From Value_W Array End

            }
            //If Value_W Table Equals "N" Run End
            else
            {

                //If Not Have The Path From A To B Get Recersive Value From a And Minimum Index Start
                for (int i = 0; i < DEEP; i++)
                {
                    if (Calculate[a, b] < DEEP)
                    {
                        if (Min > 2000000)
                            Ary[0] = (int.Parse(W(a, Min_index[i])) + Min) - int.MaxValue + 1;
                        else
                            Ary[0] = int.Parse(W(a, Min_index[i])) + Min;
                        break;
                    }
                }
                //If Not Have The Path From A To B Get Recersive Value From a And Minimum Index End

            }



            //If Value Value_W Lowwer From Value Value_D Return Value_W Start
            try
            {
                if (Ary[0] > int.Parse(Value_W[a, b]))
                    return Value_W[a, b];
            }
            catch
            {

            }
            //If Value Value_W Lowwer From Value Value_D Return Value_W End



            //If Value Value_W Bigger From Value Value_D Return Value_W
            return Ary[0].ToString();

        }
        //Function Fill Elements D From W Table Elements End

    }
}
