namespace CPUSchedulingSimulation;

public class IoRequest
{
    
 public int AtCpuTime {get; set;}
 public int Duration {get; set;}

 public IoRequest(int atCpuTime, int duration)
    {
        AtCpuTime = atCpuTime;
        Duration = duration;
    }


}