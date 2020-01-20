using pm.Data;
using pm.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pm.Bus.BusHelpers
{
    public interface IClaimJwtBus
    {
        bool SaveJwt(string token, Guid uId);
    }

    public class ClaimJwtBus : IClaimJwtBus
    {
        private prContext _ctx;

        public ClaimJwtBus(prContext ctx)
        {
            _ctx = ctx;
        }

        public bool SaveJwt(string token, Guid uId)
        {
            var claimJwt = new ClaimJwt
            {
                Jwt = token,
                UserId = uId
            };

            try
            {
                var claimJwtExists = _ctx.ClaimJwts.FirstOrDefault(x => x.UserId == uId);
                if (claimJwtExists == null)
                {
                    _ctx.Add(claimJwt);
                }
                else
                {
                    claimJwtExists.UserId = uId;
                    claimJwtExists.Jwt = token;

                    _ctx.Entry(claimJwtExists).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                _ctx.SaveChanges();

            }
            catch (Exception e)
            {
                var error = e;
                throw;
            }


            return true;
        }
    }
}
