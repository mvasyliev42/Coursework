using Coursework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Services
{
    internal abstract class Service
    {


        abstract public List<IModel> getList();
        abstract public bool save();
        abstract public bool update(int id);

    }
}
