using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoUI.Models;

namespace DemoUI.Services
{
    public interface IS_Product
    {
        List<M_Product> getAll();
        string errObj();
    }
}
