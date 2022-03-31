using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KritischeRuimteHostedPoc.Client.Services
{
    public interface IPdfService
    {
        [Post("/pdf")] Task<string> CreatePdf([Body] KritischeRuimteHostedPoc.Shared.CreatePdfRequest request);

    }
}
