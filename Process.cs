using System.Collections.Generic;

namespace CPUSchedulingSimulation;

public enum ProcessState {New, Ready, Running, Waiting, Terminated}

public class Process{

//Input//
public int Pid {get; set;}
public int ArrivalTime {get; set;}
public int BurstTime {get; set;}
public int Priority {get; set;}

public List<IoRequest> IoRequests {get; set;} = new();

//Runtime State//
public ProcessState State {get; set;} = ProcessState.New;
public int RemainingBurst {get; set;}
public int CpuTimeUsed {get; set;}
public int NextIoIndex {get; set;}
public int IoTimeRemaining {get; set;}

//Metrics//
public int StartTime {get; set;}
public int CompletionTime {get; set;}
public int ReadyQueueTime {get; set;}
public int TotalIoTime {get; set;}
public int TurnaroundTime => CompletionTime - ArrivalTime;
public int WaitingTime => TurnaroundTime - BurstTime;

public Process(int pid, int arrival, int burst, int priority)
{
    Pid = pid;
    ArrivalTime = arrival;
    BurstTime = burst;
    RemainingBurst = burst;
    Priority = priority;
}

public IoRequest GetDueIoRequest()
{
    if(NextIoIndex < IoRequests.Count && CpuTimeUsed == IoRequests[NextIoIndex].AtCpuTime)
        {
         return IoRequests[NextIoIndex];
        }
    return null;
}

public Process Clone()
{
    var copy = new Process(Pid, ArrivalTime, BurstTime, Priority);
    foreach (var io in IoRequests)
        {
            copy.IoRequests.Add(new IoRequest(io.AtCpuTime, io.Duration));
        }
    return copy;
}

}