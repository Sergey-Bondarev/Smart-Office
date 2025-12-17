using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Office.Abstract
{
    public interface ISmartControl
    {
        string DeviceId { get; }

        bool IsOnline { get; }

        void ExecuteDiagnostic();
    }
}
