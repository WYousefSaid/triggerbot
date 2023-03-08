using Trigger;

namespace Trigger.essentials
{
  

public static  class Globals
{
    public static Memory mem;
    public static IntPtr client;
    public static IntPtr engine;

    static Globals()
    {
        mem = new Memory();
        client = mem.GetModuleBase("client.dll");
        engine = mem.GetModuleBase("engine.dll");
    }
}}