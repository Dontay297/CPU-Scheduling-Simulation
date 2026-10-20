using System.Collections.Generic;
using System.Diagnostics;

namespace CPUSchedulingSimulation;

public class PriorityScheduler : IScheduler
{
    public string Name => "Priority";

    public Process? PickNext(List<Process> readyQueue)
    {

        return null;
    }

    public bool ShouldPreempt(Process running, List<Process> readyQueue, int ticksUsedThisTurn)
    {
        foreach (var p in readyQueue)
        {
            if (p.Priority< running.Priority)
            {
                return true;
            }
           
        }
        return false;
    }







}