﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMY.Services.IServices
{
    public interface ISms
    {
        void SendRegisteCode(string phone);
    }
}
