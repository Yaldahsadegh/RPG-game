using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.quest
{
    public interface IObserver
    {
        void Update(string questStatus);
    }
}
