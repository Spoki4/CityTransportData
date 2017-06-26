using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.services.database.entities.inheritance
{
    public interface IIdEntity
    {
        int Id { set; get; }
    }
}
