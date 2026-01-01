using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Commands.Commands.Vips
{
    internal class MapasDungeonPermitidos
    {

        public ulong costoTPKamas = 120000;
        public List<int> mapasPermitidos = new List<int>();
       
        
        public void Variables()
        {
            

            
            mapasPermitidos.Add(12012);
            mapasPermitidos.Add(12013);
            mapasPermitidos.Add(12014);
        }

       

    }
}
