//Tento program ulozi predmety do excel souboru a vznikne rozvrh hodin. Je mozne ho i editovat. 
//Po kazde akci se vrati do uvodniho menu.
using System.IO;
using System;
class Program
{
    static bool zapnuti = true;
    static string csvPath = "rozvrhHodin.csv";
    static void Main()
    {
        Console.Title = "ROZVRH HODIN - MENU";
        ConsoleKeyInfo stisknutiKlavesy;
        while (zapnuti == true)
        {
            obarveniZluta();
            Console.WriteLine("+------------ ROZVRH HODIN = MENU -------------+");
            obarveniReset();
            Console.WriteLine();
            obarveniModra();
            Console.WriteLine("\tINSERT - Vložit nový rozvrh\t\t");
            Console.WriteLine("\tDELETE - Smazat rozvrh\t\t");
            Console.WriteLine("\tHOME - Zobrazit rozvrh\t\t");
            Console.WriteLine("\tEND/ESC - Quit/Konec programu\t\t");
            Console.WriteLine();
            obarveniZluta();
            Console.WriteLine("+----------------------------------------------+");
            obarveniReset();
            stisknutiKlavesy = Console.ReadKey();
            switch (stisknutiKlavesy.Key) //rozhodnuti podle stisknute klavesy
            {
                case ConsoleKey.Insert:
                    pridatNovyRozvrh();
                    navratDoMenu();
                    break;
                case ConsoleKey.Delete:
                    smazatCelyRozvrh();
                    navratDoMenu();
                    break;
                case ConsoleKey.Home:
                    zobrazitRozvrh();
                    navratDoMenu();
                    break;
                case (ConsoleKey.Escape or ConsoleKey.End):
                    ukonceniProgramu();
                    return;
                default: //pokud uzivatel stiskne jinak klavesu nez je povolena,tak se menu nebude psat porad pod sebe, ale hodi to chybu
                    Console.Clear();
                    obarveniCervena();
                    Console.WriteLine("ŠPATNÁ KLÁVESA");
                    obarveniReset();
                    navratDoMenu();
                    break;
            }
        }
    }
    static void pridatNovyRozvrh()
    {
        Console.Title = "ROZVRH HODIN - NOVÝ ROZVRH";

        Console.WriteLine("Soubor se uklada");
        using (StreamWriter sw = new StreamWriter("rozvrhHodin.csv", false))
        {
            sw.WriteLine(" ;     1;     2;     3;     4;     5;     6;     7;     8"); //hlavicka v excelu (tolik mezer, aby to bylo zarovnane pri vypisu tabulky)
            string[] dny = { "PO", "ÚT", "STŘ", "ČT", "PÁ" };
            for (int den = 0; den < dny.Length; den++)
            {
                obarveniZluta();
                Console.Clear();
                Console.WriteLine("\n--- " + dny[den] + " ---");
                obarveniReset();
                string[] hodiny = new string[8];

                for (int h = 0; h < hodiny.Length; h++)
                {
                    obarveniModra();
                    Console.WriteLine((h + 1) + ". HODINA:");
                    obarveniReset();
                    hodiny[h] = Console.ReadLine();
                }
                sw.WriteLine(dny[den] + ";" + string.Join(";", hodiny));
            }
        }
        Console.Clear();
        obarveniModra();
        Console.WriteLine("SUPER - TVŮJ ROZVRH SE ULOŽIL!");
        obarveniReset();
    }
    static void zobrazitRozvrh()
    {
        Console.Clear();
        Console.Title = "ROZVRH HODIN";
        if ((!File.Exists(csvPath)))
        {
            obarveniCervena();
            Console.WriteLine("ŽÁDNÝ ROZVRH K ZOBRAZENÍ");
            obarveniReset();
        }
        else
        {
            Console.Clear();
            string[] radky = File.ReadAllLines(csvPath); //nacte excel do pole, aby se mohl vypsat
            if (radky.Length > 0) //vypise barvenou hlavicku 
            {
                obarveniCervena();
                Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------+");
                string[] hlavicka = radky[0].Split(';');
                for (int j = 0; j < hlavicka.Length; j++)
                {
                    Console.Write(" " + hlavicka[j].PadRight(10) + " |");
                }
                Console.WriteLine();
                Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------+");
                Console.ResetColor();//reset barvy, nebyla pouzita metoda protoze v metode je navic Console.WriteLine(), coz delalo chybicku mensi no takze jsem se na to vykaslal
            }
            for (int i = 1; i < radky.Length; i++) //vypsani zbytku
            {
                string[] jednoPolickuVRozvrhu = radky[i].Split(';');
                for (int j = 0; j < jednoPolickuVRozvrhu.Length; j++)
                {
                    Console.Write(" " + jednoPolickuVRozvrhu[j].PadRight(10) + " |");
                }
                Console.WriteLine();
                Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------+");
            }
        }
    }
    static void smazatCelyRozvrh()
    {
        Console.Title = "ROZVRH HODIN - MAZÁNÍ";
        if (!File.Exists(csvPath)) //kontrola, jestli vubec rozvrh existuje
        {
            Console.Clear();
            obarveniCervena();
            Console.WriteLine("ŽÁDNÝ ROZVRH K SMAZÁNÍ");
            obarveniReset();
        }
        else
        {
            Console.Clear();
            File.Delete(csvPath);
            obarveniModra();
            Console.WriteLine("ROZVRH BYL SMAZÁN");
            obarveniReset();
        }
    }
    static void navratDoMenu()
    {
        Console.WriteLine();
        obarveniZluta();
        Console.WriteLine("\t\t\t\tPRO NÁVRAT DO MENU STISKNI ENTER");
        obarveniReset();
        Console.ReadLine();
        Console.Clear();
    }
    static void ukonceniProgramu()
    {
        Console.Title = "ROZVRH HODIN - UKONČEN";
        obarveniCervena();
        Console.WriteLine("UKONČUJI PROGRAM...");
        zapnuti = false;
    }
    //metody, ktere upravuji barvy textu
    static void obarveniZluta()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
    }
    static void obarveniCervena()
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
    static void obarveniModra()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
    }
    static void obarveniReset()
    {
        Console.ResetColor();
    }
}
