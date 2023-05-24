namespace ProductsPricing.Api.NcmImport
{
    public class NCMItem
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }

    public class NCM
    {
        public IEnumerable<NCMItem> Nomenclaturas { get; set; }
    }

}
