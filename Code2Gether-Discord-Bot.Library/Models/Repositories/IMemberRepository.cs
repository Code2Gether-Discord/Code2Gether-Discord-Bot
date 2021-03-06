﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public interface IMemberRepository : IDataRepository<Member>
    {
        public Task<Member> ReadFromSnowflakeAsync(ulong snowflakeId);
    }
}
