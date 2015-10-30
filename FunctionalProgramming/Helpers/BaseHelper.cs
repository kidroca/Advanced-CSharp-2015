using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkHelpers
{
    public abstract class BaseHelper
    {
        private ConsoleHelper console;

        public ConsoleHelper ConsoleMio
        {
            get
            {
                if (this.console == null)
                {
                    this.console = new ConsoleHelper();
                }

                return this.console;
            }
        }
    }
}
