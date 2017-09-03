using AutoMapper;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using TPS.Web.Core;
using TPS.Web.Core.Dtos;
using TPS.Web.Core.Models;

namespace TPS.Web.Controllers.Api
{
    [Authorize]
    public class AuditsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Audits
        public IHttpActionResult GetAuditRecords()
        {
            var audits = _unitOfWork.AuditRecords.GetAudits();

            return Ok(Mapper.Map<IEnumerable<Audit>, IEnumerable<AuditDto>>(audits));
        }

        // GET: api/Audits/5
        [ResponseType(typeof(Audit))]
        public IHttpActionResult GetAudit(string id)
        {
            var audit = _unitOfWork.AuditRecords.GetAudit(id);
            if (audit == null)
                return NotFound();

            return Ok(Mapper.Map<Audit, AuditDto>(audit));
        }
    }
}