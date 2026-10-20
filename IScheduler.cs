using System.Collections.Generic;
namespace CPUSchedulingSimulation;

public interface IScheduler
{
    string Name {get;}

    Process? PickNext(List<Process> readyQueue);

    bool ShouldPreempt(Process running, List<Process> readyQueue, int ticksUsedThisTurn);
    
}