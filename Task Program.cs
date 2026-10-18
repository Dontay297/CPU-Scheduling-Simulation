using System;
using System.IO.Pipelines;
using System.Collections.Generic;


class TaskProgram
{
    static void Main()
    {
      int choice = 0;
      Console.WriteLine("Hello, welcome to the CPU Task Schedluling Program");
      

    do{
      Console.WriteLine("==== Main Tasks ====");
      Console.WriteLine("1.FIRST COME FIRST SERVE");
      Console.WriteLine("2.PRIORITY");
      Console.WriteLine("3.ROUND ROBIN");
      Console.WriteLine("4 EXIT");
      Console.WriteLine("Tell me, what type of algorithm do you want use ?");

      switch (choice)
      {
        case 1: FCFS();
        break;

        case 2: Priority();
        break;

        case 3: RR();
        break;

        case 4: Console.WriteLine("Okay, Exting menu...");
        break;

        default: Console.WriteLine("Invalid number, pick a number from 1 - 3 or hit 4 to exit");
        break;

      }
      }

   while(choice != 4);

  }
    
}