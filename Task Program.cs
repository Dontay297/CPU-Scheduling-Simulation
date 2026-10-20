using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
namespace CPUSchedulingSimulation;

class TaskProgram
{
  static List<Process> _processes = new();
    static void Main()
    {
      EngineTests.RunAll();
      //Remeber to add processes.cvs data here//
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
    


static void FCFS()
  {
    runSimulation();
  }

static void Priority()
  {
    runSimulation();
  }

static void RR()
  {
    runSimulation();
  }

 static void runSimulation(IScheduler scheduler)
  {
    
  }

  static void printResults()
  {
    
  }

}