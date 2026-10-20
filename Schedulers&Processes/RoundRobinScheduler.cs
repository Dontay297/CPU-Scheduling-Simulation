using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualBasic;

namespace CPUSchedulingSimulation;

public class RoundRobinScheduler : IScheduler
{
    public string Name => "Round Robin";
    private readonly int _quantum;

    public RoundRobinScheduler(int quantum)
    {
        _quantum = quantum;
    }

    public Process? PickNext(List<Process> readyQueue)
    {

        return null;
    }

    public bool ShouldPreempt(Process running, List<Process> readyQueue, int ticksUsedThisTurn)
    {
        
        return false;
    }







}