﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenaltyMaster3000.Navigation
{
    public interface INavigationService
    {
        void NavigateTo(string pageKey);
    }
}
