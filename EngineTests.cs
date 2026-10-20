using System;
using System.Collections.Generic;
using System.Linq;

namespace CPUSchedulingSimulation;

public static class EngineTests
{
    static int _failures = 0;

    public static void RunAll()
    {
        Test1_SingleProcess();
        Test2_SingleProcessWithIo();
        Test3_ThreeProcessesFcfs();
        Test4_InstructorExample();

        Console.WriteLine();
        Console.WriteLine(_failures == 0 ? "ALL TESTS PASSED" : $"{_failures} CHECK(S) FAILED");
    }

    static void Test1_SingleProcess()
    {
        Console.WriteLine("Test 1: one process, burst 3, no I/O");
        var list = new List<Process> { new Process(1, 0, 3, 1) };

        var done = new SimulationEngine(new FcfsScheduler()).Run(list);

        var p1 = done.First(p => p.Pid == 1);
        Check("P1 completion", p1.CompletionTime, 3);
        Check("P1 turnaround", p1.TurnaroundTime, 3);
        Check("P1 waiting", p1.WaitingTime, 0);
    }

    static void Test2_SingleProcessWithIo()
    {
        Console.WriteLine("Test 2: one process, burst 6, I/O after 2 ticks lasting 3");
        var p = new Process(1, 0, 6, 1);
        p.IoRequests.Add(new IoRequest(2, 3));
        var list = new List<Process> { p };

        var done = new SimulationEngine(new FcfsScheduler()).Run(list);

        var p1 = done.First(x => x.Pid == 1);
        Check("P1 completion", p1.CompletionTime, 9);
        Check("P1 total I/O time", p1.TotalIoTime, 3);
        Check("P1 ready queue time", p1.ReadyQueueTime, 0);
    }

    static void Test3_ThreeProcessesFcfs()
    {
        Console.WriteLine("Test 3: arrivals 0,1,2 with bursts 5,3,2, no I/O");
        var list = new List<Process>
        {
            new Process(1, 0, 5, 1),
            new Process(2, 1, 3, 1),
            new Process(3, 2, 2, 1)
        };

        var done = new SimulationEngine(new FcfsScheduler()).Run(list);

        var p1 = done.First(p => p.Pid == 1);
        var p2 = done.First(p => p.Pid == 2);
        var p3 = done.First(p => p.Pid == 3);

        Check("P1 completion", p1.CompletionTime, 5);
        Check("P2 completion", p2.CompletionTime, 8);
        Check("P3 completion", p3.CompletionTime, 10);
        Check("P1 waiting", p1.WaitingTime, 0);
        Check("P2 waiting", p2.WaitingTime, 4);
        Check("P3 waiting", p3.WaitingTime, 6);
        Check("P2 ready queue time", p2.ReadyQueueTime, 4);
        Check("P3 ready queue time", p3.ReadyQueueTime, 6);
    }

    static void Test4_InstructorExample()
{
    Console.WriteLine("Test 4: instructor example (shared I/O, preemptive FCFS)");
    var p1 = new Process(1, 0, 10, 1); p1.IoRequests.Add(new IoRequest(2, 3));
    var p2 = new Process(2, 2, 6, 1);  p2.IoRequests.Add(new IoRequest(1, 2));
    var p3 = new Process(3, 4, 8, 1);  p3.IoRequests.Add(new IoRequest(3, 1));

    var done = new SimulationEngine(new FcfsScheduler())
        .Run(new List<Process> { p1, p2, p3 });

    Check("P1 completion", done.First(p => p.Pid == 1).CompletionTime, 13);
    Check("P2 completion", done.First(p => p.Pid == 2).CompletionTime, 18);
    Check("P3 completion", done.First(p => p.Pid == 3).CompletionTime, 26);
    Check("P1 ready queue time", done.First(p => p.Pid == 1).ReadyQueueTime, 0);
    Check("P2 ready queue time", done.First(p => p.Pid == 2).ReadyQueueTime, 6);
    Check("P3 ready queue time", done.First(p => p.Pid == 3).ReadyQueueTime, 13);
}

    static void Check(string label, int actual, int expected)
    {
        if (actual == expected)
        {
            Console.WriteLine($"  PASS  {label} = {actual}");
        }
        else
        {
            Console.WriteLine($"  FAIL  {label}: expected {expected}, got {actual}");
            _failures++;
        }
    }
}