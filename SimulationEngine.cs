using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ExceptionServices;



namespace CPUSchedulingSimulation;

public class SimulationEngine
{
    private readonly IScheduler _scheduler;

    public SimulationEngine(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    public List<Process> Run(List<Process> processes)
    {
        int time = 0;
        var notArrived = new List<Process>(processes);
        var readyQueue = new List<Process>();
        var ioQueue = new List<Process>();
        var finished = new List<Process>();
        Process running = null;
        Process ioDevice = null;
        int ticksThisTurn = 0;

        
        while (finished.Count < processes.Count)
        {
            for (int i = notArrived.Count -1; i >= 0; i--)
            {
             if (notArrived[i].ArrivalTime == time)
                {
                    var p = notArrived[i];
                    p.State = ProcessState.Ready;
                    readyQueue.Add(p);
                    notArrived.RemoveAt(i);
                }
            }

            if(ioDevice != null && ioDevice.IoTimeRemaining == 0)
            {
                ioDevice.State = ProcessState.Ready;
                readyQueue.Add(ioDevice);
                ioDevice = null;
            }
            if (ioDevice == null && ioQueue.Count > 0)
            {
                ioDevice = ioQueue[0];
                ioQueue.RemoveAt(0);
            }

            if (running != null)
            {
                
                if (_scheduler.ShouldPreempt(running,readyQueue,ticksThisTurn)){
                    running.State = ProcessState.Ready;
                    readyQueue.Add(running);
                    running = null;
            }

            }


            if (running == null)
            {
                var next = _scheduler.PickNext(readyQueue);
               
               if (next != null){
                readyQueue.Remove(next);
                next.State = ProcessState.Running;
                running = next;
                ticksThisTurn = 0;

                if (next.StartTime == -1)
                {
                    next.StartTime = time;
                } 
                
             }
                
            }

            if (running != null)
            {
                running.RemainingBurst -= 1;
                running.CpuTimeUsed += 1;
                ticksThisTurn += 1;
            }

            foreach(var p in readyQueue)
            {
                p.ReadyQueueTime += 1;
            }   
            
            if(ioDevice != null)
            {
                ioDevice.TotalIoTime += 1;
                ioDevice.IoTimeRemaining -= 1;
            }   


            if (running != null)
            {
                if (running.RemainingBurst == 0){
                
                running.CompletionTime = time + 1;
                running.State = ProcessState.Terminated;
                finished.Add(running);
                running = null;
                
            }
            else
            {
                var io = running.GetDueIoRequest();
                if(io != null)
                {
                    running.IoTimeRemaining = io.Duration;
                    running.NextIoIndex += 1;
                    running.State = ProcessState.Waiting;
                    ioQueue.Add(running);
                    running = null;
                }
            }
            
            }
          time += 1;
        }
        

     return finished;
    }

}