using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgressClock
{
    public int length{ get; private set; }
    public int ticks { get; private set; }
    public UnityEvent<ProgressClock> onClockFull;
    public UnityEvent<ProgressClock> onClockEmpty;
    public UnityEvent<ProgressClock> onClockTick;
    public UnityEvent<ProgressClock> onClockExtended;

    public ProgressClock(int size, int ticks = 0)
    {
        this.length = size;
        this.ticks = ticks;

        onClockFull = new UnityEvent<ProgressClock>();
        onClockEmpty = new UnityEvent<ProgressClock>();
        onClockTick = new UnityEvent<ProgressClock>();
        onClockExtended = new UnityEvent<ProgressClock>();
    }

    public void Tick(int effect)
    {
        ticks += effect;
        onClockTick?.Invoke(this);
        if (ticks >= length)
        {
            ticks = length;
            onClockFull?.Invoke(this);
        }
        else if (ticks <= 0)
        {
            ticks = 0;
            onClockEmpty?.Invoke(this);
        }

    }

    public void Extend(int value){
        if(length + value <= 0){
            length = 1;
        }
        else{
            length += value;
        }

        if(ticks >= length){
            ticks = length;
            onClockFull?.Invoke(this);
        }

        onClockExtended?.Invoke(this);
    }
}
