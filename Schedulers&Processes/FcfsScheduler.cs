using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace CPUSchedulingSimulation;

public class FcfsScheduler : IScheduler
{
    public string Name => "FCFS";

    public Process PickNext(List<Process> readyQueue)
    {
       Process best = null;
       foreach (var p in readyQueue)
        {
            if(best == null || p.ArrivalTime < best.ArrivalTime || (p.ArrivalTime == best.ArrivalTime && p.Pid < best.Pid))
            {
                best = p;
            }
        }
        return best;
        
    }

    public bool ShouldPreempt(Process running, List<Process> readyQueue, int ticksUsedThisTurn)
    {
        foreach(var p in readyQueue)
        {
            if(p.ArrivalTime < running.ArrivalTime) return true;
        }
        return false;
    }







}