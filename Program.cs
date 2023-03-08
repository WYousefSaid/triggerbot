using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Trigger;

using static Trigger.essentials.Netvars;
using static Trigger.essentials.signatures;

namespace Trigger
{
    internal  class Program
    {
        
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);
        static void Main(string[] args)
        {
            Memory mem = new Memory();

            mem.GetProcess("csgo");
            var client = mem.GetModuleBase("client.dll");
            var engine = mem.GetModuleBase("engine.dll");

            while (true)
            {
                if ((GetAsyncKeyState(0x12) & 0x8000) != 0)
                {
                    Console.WriteLine("Triggerbot enabled");
                    var buffer = mem.ReadPointer(client, dwLocalPlayer);
                    var crossHair = BitConverter.ToInt32(mem.ReadBytes(buffer, m_iCrosshairId,4), 0);
                    var myTeam = BitConverter.ToInt32(mem.ReadBytes(buffer,m_iTeamNum,4), 0);
                    var enemy = mem.ReadPointer(client, dwEntityList + (crossHair - 1) * 0x10);
                    var enemyTeam = BitConverter.ToInt32(mem.ReadBytes(enemy, m_iTeamNum,4), 0);
                    var enemyHealth = BitConverter.ToInt32(mem.ReadBytes(enemy, m_iHealth,4), 0);
                    if (myTeam != enemyTeam && enemyHealth > 0)
                    {
                        mem.WriteBytes(client, dwForceAttack, BitConverter.GetBytes(5));
                        Thread.Sleep(1);
                        mem.WriteBytes(client, dwForceAttack, BitConverter.GetBytes(4));
                        
                    }
                }   
                
                Thread.Sleep(2);
            }

        }
    }
};