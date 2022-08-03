﻿using System;

namespace Win_Dev.Data.Dto
{
    class PersonelToProjects
    {
        public Guid PersonID { get; set; }
        public Guid ProjectID { get; set; }

        public virtual Person Person { get; set; }
        public virtual Project Project { get; set; }
    }
}
