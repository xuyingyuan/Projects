using Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commands.Data
{
    public class MokCommanderRepo : ICommanderRepo
    {
        public Command GetCommandById()
        {
            return new Command { Id = 0, HowTo = "boild an ", Line = "boil 0", Platform = "kettle" };
        }

        public IEnumerable<Command> GetCommands()
        {
            var commands = new List<Command> {
                new Command {Id = 0, HowTo = "boild an ", Line = "boil 0", Platform = "kettle" },
                new Command {Id = 1, HowTo = "boild an 1", Line = "boil 1", Platform = "kettle1" },
                new Command {Id = 2, HowTo = "boild an2 ", Line = "boil 2", Platform = "kettle2" },
                new Command {Id = 3, HowTo = "boild an3 ", Line = "boil 3", Platform = "kettle3" }
            };
            return commands;


            
        }
    }
}
