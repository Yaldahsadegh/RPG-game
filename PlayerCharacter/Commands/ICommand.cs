using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.PlayerCharacter.Commands
{
    public interface ICommand
    {
        void Execute(Character character);
    }
}
