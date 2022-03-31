using KritischeRuimteHostedPoc.Shared;

namespace KritischeRuimteHostedPoc.Server.Services
{
    public interface IPdfFormCreator
    {
       string Create(CreatePdfRequest formulierModel);
    }
}